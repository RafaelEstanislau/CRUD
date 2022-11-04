sap.ui.define([
	"sap/ui/base/ManagedObject"
], function (
	ManagedObject,

) {
	"use strict";

	return ManagedObject.extend("sap.ui.demo.walkthrough.Validacao", {
		_i18n: null,
		Receberi18n: function(pacote){
			this._i18n = pacote
		},
		ValidarCadastro: function (inputsDeCampo, inputData) {
			let erroDeData = false;
			let erroDeInput = false;
			let erroDeValidacao = false;

			inputsDeCampo.forEach(input =>
				erroDeInput = this._validarCampo(input) || erroDeInput, this);
			erroDeData = this._validarData(inputData);

			if(erroDeData || erroDeInput == true){
				erroDeValidacao = true;
			}
			return erroDeValidacao
		},
		_validarCampo: function (input) {
			const mensagemValidacaoDeCampo = this._i18n.getText("mensagemValidacaoDeCampo")
			let estado = "None";
			let erroDeValidacao = false;
			let oBinding = input.getBinding("value");
			try {
				oBinding.getType().validateValue(input.getValue());
			} catch (oException) {
				estado = "Error";
				erroDeValidacao = true;
			}
			
			input.setValueStateText(mensagemValidacaoDeCampo);
			input.setValueState(estado);
			return erroDeValidacao;
		},

		_validarData: function (inputData) {
			const mensagemValidacaoDeData = this._i18n.getText("mensagemValidacaoDeData")
			let dataInputada = inputData.getValue();
			let estado = "None";
			let erroDeValidacao = false;
			let dataMinimaValida = new Date(1860, 1, 1).toISOString();
			let dataMaximaValida = new Date().toISOString();

			if (dataInputada.length == 0) {
				estado = "Error"
				erroDeValidacao = true;
			}
			try {
				var dataInputadaFormatada = new Date(dataInputada).toISOString();
				if (dataInputadaFormatada > dataMinimaValida && dataInputadaFormatada < dataMaximaValida) {
					erroDeValidacao = false;
					estado = "None";
				} else {
					erroDeValidacao = true;
					estado = "Error";
				}
			} catch (oException) {
				estado = "Error";
				erroDeValidacao = true;
			}
			inputData.setValueState(estado);
			inputData.setValueStateText(mensagemValidacaoDeData);
			return erroDeValidacao;
		},
	});
});