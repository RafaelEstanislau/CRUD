sap.ui.define([
	"sap/ui/base/ManagedObject"
], function(
	ManagedObject,

) {
	"use strict";

	return ManagedObject.extend("sap.ui.demo.walkthrough.controller.Validacao", {
		
        ValidarCampo: function (input) {
			var estado = "None";
			var erroDeValidacao = false;
			var oBinding = input.getBinding("value");
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

		ValidarData : function(inputData){
			var dataInputada = inputData.getValue();
			var estado = "None";
			var erroDeValidacao = false;
			let dataMinimaValida = new Date(1860, 1, 1).toISOString();
			let dataMaximaValida = new Date().toISOString();

			if(dataInputada.length == 0){
				estado = "Error"
				erroDeValidacao = true;
			}
			try {
				var dataInputadaFormatada = new Date(dataInputada).toISOString();
				if(dataInputadaFormatada > dataMinimaValida && dataInputadaFormatada < dataMaximaValida){
					erroDeValidacao = false;
					estado = "None";
				}else{
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
		}
	});
});