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
			
			evento.getParameter(parametroNome) == rotaEditarLivro
				?	this._carregarLivro(window.decodeURIComponent(evento.getParameter("arguments").id))
				:	this.getView().setModel(new JSONModel(), nomeModelo);
		},

		_carregarLivro: function (id) {
			const nomeModelo = "livro";
			let _repositorioLivro = new RepositorioDeLivros;

			_repositorioLivro.BuscarLivroPorId(id).then(livroRetornado => {
				let oModel = new JSONModel(livroRetornado);
				this.getView().setModel(oModel, nomeModelo);
			});
		},
		//paramos aqui

		AoClicarEmSalvar: function () {
			let _validacao = new Validacao;

			var telaCadastro = this.getView(),
				inputs = [
					telaCadastro.byId(inputTitulo),
					telaCadastro.byId(inputEditora),
					telaCadastro.byId(inputAutor),
				],
				erroDeValidacaoDeCampos = false;
			inputs.forEach(input =>
				erroDeValidacaoDeCampos = _validacao.ValidarCampo(input) || erroDeValidacaoDeCampos, this);
			const dateTimePicker = "DT";
			let erroDeValidacaoDeData = _validacao.ValidarData(this.getView().byId(dateTimePicker));
			let livroASerSalvo = this.getView().getModel("livro");

			if (!erroDeValidacaoDeCampos && !erroDeValidacaoDeData) {
				let _repositorioLivro = new RepositorioDeLivros;
				let livroModelo = livroASerSalvo.getData();
				const rota = "detalhes";
				if (!!livroModelo.id) {
					_repositorioLivro.AtualizarLivro(livroASerSalvo)
						.then(livro => {
							let idDoLivro = livro.id;
							this._navegarParaRota(rota, idDoLivro);
						})
				} else {
					_repositorioLivro.SalvarLivro(livroASerSalvo)
						.then(livro => {
							let idDoLivro = livro.id;
							this._navegarParaRota(rota, idDoLivro);
						})
				}
			} else {
				MessageBox.alert("Falha na validação dos campos");
			}
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
					let parametroDaRota = null;
					if (confirmacao === 'OK') {
						this._navegarParaRota(rota, parametroDaRota);
					}
				}.bind(this)
			});
		},

		_navegarParaRota(nomeDaRota, parametroDaRota = null) {
			let rota = this.getOwnerComponent().getRouter();
			(parametroDaRota !== null) 
				? rota.navTo(nomeDaRota, {
					"id": parametroDaRota
				})
				: rota.navTo(nomeDaRota) 
		}
	});
});