sap.ui.define([
	"sap/ui/base/ManagedObject"
], function (
	ManagedObject,

) {
	"use strict";

	return ManagedObject.extend("sap.ui.demo.walkthrough.controller.Validacao", {

		ValidarCadastro: function (inputsDeCampo, data) {
			let erroDeInput = false;
			let erroDeData = false;

			inputsDeCampo.forEach(input =>
				erroDeInput = this._validarCampo(input) || erroDeInput, this);
			erroDeData = this._validarData(data);
			return {
				erroDeInput,
				erroDeData
			};
		},

		_validarCampo: function (input) {
			let estado = "None";
			let erroDeValidacao = false;
			let oBinding = input.getBinding("value");
			try {
				oBinding.getType().validateValue(input.getValue());
			} catch (oException) {
				estado = "Error";
				erroDeValidacao = true;
			}
			input.setValueStateText("O campo deve conter 1-100 caracteres");
			input.setValueState(estado);
			return erroDeValidacao;
		},

		_validarData: function (inputData) {
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
			inputData.setValueStateText("A data deve ser válida e preenchida entre 1860 e hoje");
			return erroDeValidacao;
		},
	});
});