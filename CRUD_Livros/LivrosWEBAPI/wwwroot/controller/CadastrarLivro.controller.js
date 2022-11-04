sap.ui.define([
	"sap/ui/core/mvc/Controller",
	"sap/m/MessageBox",
	"sap/ui/model/json/JSONModel",
	"sap/ui/core/Core",
	"sap/ui/demo/walkthrough/Validacao",
	"sap/ui/demo/walkthrough/controller/RepositorioDeLivros",
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
	const caminhoDoCadastro = "sap.ui.demo.walkthrough.controller.CadastrarLivro"
	return Controller.extend(caminhoDoCadastro, {
		_validacaoLivro: null,
		onInit: function () {
			const rotaDeCadastro = "cadastrarLivro";
			const rotaDeEditar = "editarLivro";
			this._validacaoLivro = new Validacao;

			let roteador = this
				.getOwnerComponent()
				.getRouter();
			roteador
				.getRoute(rotaDeCadastro)
				.attachPatternMatched(this._coincidirRotaDeCriacao, this);
			roteador
				.getRoute(rotaDeEditar)

				.attachPatternMatched(this._coincidirRotaDeEdicao, this);
			let tela = this.getView(),

				oMM = Core.getMessageManager();
			oMM.registerObject(tela.byId(inputTitulo), true)
			oMM.registerObject(tela.byId(inputEditora), true)
			oMM.registerObject(tela.byId(inputAutor), true)
			var i18nModel = this.getOwnerComponent().getModel("i18n").getResourceBundle();
			this._validacaoLivro.Receberi18n(i18nModel);
		},
		_processarEvento: function (acao) {
			try {
				var promise = acao();
				if (promise && typeof (promise["catch"]) == "function") {
					promise.catch(error => MessageBox.error(error.message));
				}
			} catch (error) {
				MessageBox.error(error.message);
			}
		},
		_coincidirRotaDeCriacao: function () {
			this._processarEvento(() => {
				let nomeModelo = "livro";
				this.getView().setModel(new JSONModel(), nomeModelo);
			});
		},
		_coincidirRotaDeEdicao: function (evento) {
			const erroIdInvalido = "ID inválido"
			this._processarEvento(() => {
				let idLivroASerAtualizado = evento.getParameter("arguments").id;
				if (!idLivroASerAtualizado) {
					throw new Error(erroIdInvalido)
				}
				return this._carregarLivro(idLivroASerAtualizado);
			})
		},

		_carregarLivro: function (id) {
			const nomeModelo = "livro";
			let _repositorioLivro = new RepositorioDeLivros;

			return _repositorioLivro.BuscarLivroPorId(id).then(livroRetornado => {
				let modelo = new JSONModel(livroRetornado);
				this.getView().setModel(modelo, nomeModelo);
			});
		},

		AoClicarEmSalvar: function () {
			this._processarEvento(() => {
				const dateTimePicker = "DT";
				const nomeDoModelo = "livro";
				const modeloi18n = "i18n";
				const mensagemFalhaDeValidacao = this.getView().getModel(modeloi18n).getResourceBundle().getText("mensagemFalhaDeValidacao");
				let telaCadastro = this.getView();

				let inputs = [
					telaCadastro.byId(inputTitulo),
					telaCadastro.byId(inputEditora),
					telaCadastro.byId(inputAutor),
				];

				let valorInputData = this.getView().byId(dateTimePicker);
				let retornoValidacao = this._validacaoLivro.ValidarCadastro(inputs, valorInputData);

				// if (!!retornoValidacao) {
				// 	MessageBox.alert(mensagemFalhaDeValidacao);
				// 	return
				// }
				let livroASerSalvo = this.getView().getModel(nomeDoModelo).getData();
				return !livroASerSalvo.id ?
					this._salvarLivro(livroASerSalvo) :
					this._atualizarLivro(livroASerSalvo);
			})

		},

		AoClicarEmVoltar: function (evento) {
			const rotaDaLista = "listaDeLivros";
			this._processarEvento(() => {
				this._confirmarRetornoDeNavegacao(rotaDaLista);
			})
		},

		_confirmarRetornoDeNavegacao: function (rota) {
			const modeloi18n = "i18n";
			const confirmacaoDeRetorno = this.getView().getModel(modeloi18n).getResourceBundle().getText("mensagemConfirmacaoVoltar");
			MessageBox.confirm(confirmacaoDeRetorno, {
				title: "Confirmação",
				emphasizedAction: sap.m.MessageBox.Action.OK,
				actions: [sap.m.MessageBox.Action.OK,
					sap.m.MessageBox.Action.CANCEL
				],
				onClose: (confirmacao) => {
					if (confirmacao === 'OK') {
						this._navegarParaRota(rota, null);
					}
				}
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
				.then(retorno =>
					!livroASerSalvo.ok
					? this._mensagemDeErro(retorno)
					: this._navegarParaRota(rotaDetalhes, livroASerSalvo.id));
					
		},
		_mensagemDeErro: function(textoRetorno){
			if(!!textoRetorno.detail){
				MessageBox.error(textoRetorno.detail)
			}
		},
		_salvarLivro: function (livroASerSalvo) {
			let _repositorioLivro = new RepositorioDeLivros;
			return _repositorioLivro.SalvarLivro(livroASerSalvo)
				.then(livroRetorno => {
					!livroRetorno.id
					? this._mensagemDeErro(livroRetorno)
					: this._navegarParaRota(rotaDetalhes, livroRetorno.id)
				});
		},
	});
});

//Receber o retorno da requisição 
//Checar se houve sucesso na conclusão da ação
//Se não houver, emitir o titulo do erro na tela para o usuário
