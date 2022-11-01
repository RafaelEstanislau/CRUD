sap.ui.define([
	"sap/ui/core/mvc/Controller",
	"sap/m/MessageBox",
	"sap/ui/model/json/JSONModel",
	"sap/ui/core/Core",
	"sap/ui/demo/walkthrough/Validacao",
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
			let roteador = this
				.getOwnerComponent()
				.getRouter();
			roteador
				.getRoute("cadastrarLivro")
				.attachPatternMatched(this._coincidirRotaDeCriacao, this);
			roteador
				.getRoute("editarLivro")

				.attachPatternMatched(this._coincidirRotaDeEdicao, this);
			let tela = this.getView(),

				oMM = Core.getMessageManager();
			oMM.registerObject(tela.byId(inputTitulo), true)
			oMM.registerObject(tela.byId(inputEditora), true)
			oMM.registerObject(tela.byId(inputAutor), true)
		},
		_processarEvento: function(acao){
			try {
				var promise = acao();
				if(promise && typeof(promise["catch"]) == "function"){
					promise.catch(error => MessageBox.error(error.message));
				}
			}catch (error) {
				MessageBox.error(error.message);
			}
		},
		_coincidirRotaDeCriacao: function(evento){
		    this._processarEvento(() => {
				let nomeModelo = "livro";
				this.getView().setModel(new JSONModel(), nomeModelo);
			});
		},
		_coincidirRotaDeEdicao: function(evento){
			this._processarEvento(() => {
				let idLivroASerAtualizado = evento.getParameter("arguments").id;
				if(!idLivroASerAtualizado){
					throw new Error("Id inválido")
				}
				return this._carregarLivro(idLivroASerAtualizado);
			})
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
			let retornoValidacao = _validacaoLivro.ValidarCadastro(inputs, valorInputData); 
			let livroASerSalvo = this.getView().getModel(nomeDoModelo).getData();

			!retornoValidacao.erroDeInput && !retornoValidacao.erroDeData ?
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