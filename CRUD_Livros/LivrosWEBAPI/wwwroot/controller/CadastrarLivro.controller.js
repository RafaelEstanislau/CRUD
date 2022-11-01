sap.ui.define([
	"sap/ui/core/mvc/Controller",
	"sap/m/MessageBox",
	"sap/ui/model/json/JSONModel",
	"sap/ui/core/Core",
	"sap/ui/demo/walkthrough/controller/Validacao",
	"sap/ui/demo/walkthrough/controller/RepositorioDeLivros"

], function (
	Controller,
	MessageBox,
	JSONModel,
	Core,
	Validacao,
	RepositorioDeLivros) {
	"use strict";

	const inputTitulo = "input-titulo";
	const inputAutor = "input-autor";
	const inputEditora = "input-editora";
	const rotaDetalhes = "detalhes";

	return Controller.extend("sap.ui.demo.walkthrough.controller.CadastrarLivro", {

		onInit: function () {

			let rota = sap.ui.core.UIComponent.getRouterFor(this);
			rota.attachRoutePatternMatched(this._coincidirRota, this);
			let tela = this.getView(),

				oMM = Core.getMessageManager();
			oMM.registerObject(tela.byId(inputTitulo), true)
			oMM.registerObject(tela.byId(inputEditora), true)
			oMM.registerObject(tela.byId(inputAutor), true)
		},

		_coincidirRota: function (evento) {
			const parametroNome = "name";
			const rotaEditarLivro = "editarLivro";
			const nomeModelo = "livro";
			let idLivroASerAtualizado = window.decodeURIComponent(evento.getParameter("arguments").id);

			evento.getParameter(parametroNome) == rotaEditarLivro ?
				this._carregarLivro(idLivroASerAtualizado) :
				this.getView().setModel(new JSONModel(), nomeModelo);
		},

		_carregarLivro: function (id) {
			const nomeModelo = "livro";
			let _repositorioLivro = new RepositorioDeLivros;

			_repositorioLivro.BuscarLivroPorId(id).then(livroRetornado => {
				let oModel = new JSONModel(livroRetornado);
				this.getView().setModel(oModel, nomeModelo);
			});
		},

		AoClicarEmSalvar: function () {
			const dateTimePicker = "DT";
			const nomeDoModelo = "livro";
			let _validacaoLivro = new Validacao;
			let telaCadastro = this.getView();

			let inputs = [
				telaCadastro.byId(inputTitulo),
				telaCadastro.byId(inputEditora),
				telaCadastro.byId(inputAutor),
			];

			let valorInputData = this.getView().byId(dateTimePicker);
			let erroDeValidacaoDeCampos = _validacaoLivro.ValidarCadastro(inputs, valorInputData).erroDeInput;
			let erroDeValidacaoDeData = _validacaoLivro.ValidarCadastro(inputs, valorInputData).erroDeData;
			let livroASerSalvo = this.getView().getModel(nomeDoModelo).getData();

			!erroDeValidacaoDeCampos && !erroDeValidacaoDeData ?
				!livroASerSalvo.id ?
				this._salvarLivro(livroASerSalvo) :
				this._atualizarLivro(livroASerSalvo) :
				MessageBox.alert("Falha na validação dos campos");
		},

		AoClicarEmVoltar: function () {
			const rotaDaLista = "listaDeLivros";
			this._confirmarRetornoDeNavegacao(rotaDaLista);
		},

		_confirmarRetornoDeNavegacao: function (rota) {
			MessageBox.confirm("Ao voltar todas as alterações serão perdidas. Deseja continuar?", {
				title: "Confirmação",
				emphasizedAction: sap.m.MessageBox.Action.OK,
				actions: [sap.m.MessageBox.Action.OK,
					sap.m.MessageBox.Action.CANCEL
				],
				onClose: function (confirmacao) {
					if (confirmacao === 'OK') {
						this._navegarParaRota(rota, null);
					}
				}.bind(this)
			});
		},

		_navegarParaRota(nomeDaRota, id = null) {
			let rota = this.getOwnerComponent().getRouter();

			!!id
				?
				rota.navTo(nomeDaRota, {
					"id": id
				}) :
				rota.navTo(nomeDaRota);
		},

		_atualizarLivro: function (livroASerSalvo) {
			let _repositorioLivro = new RepositorioDeLivros;
			return _repositorioLivro.AtualizarLivro(livroASerSalvo)
				.then(this._navegarParaRota(rotaDetalhes, livroASerSalvo.id));
		},

		_salvarLivro: function (livroASerSalvo) {
			let _repositorioLivro = new RepositorioDeLivros;
			return _repositorioLivro.SalvarLivro(livroASerSalvo)
				.then(livroRetorno => {
					this._navegarParaRota(rotaDetalhes, livroRetorno.id)
				});
		},
	});
});