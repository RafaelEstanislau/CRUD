sap.ui.define([
	"sap/ui/core/Core",
	"sap/ui/base/ManagedObject",
	"sap/ui/demo/walkthrough/controller/CadastrarLivro"
], function(
	Core,
	ManagedObject,
	CadastrarLivro
) {
	"use strict";

	return CadastrarLivro.extend("sap.ui.demo.walkthrough.controller.Validacao", {
		
        validarCampo: function (input) {
			var estado = "None";
			var erroDeValidacao = false;
			var oBinding = input.getBinding("value");
			

			let dataMinimaValida = new Date(1860, 1, 1).toISOString();
			let dataMaximaValida = new Date().toISOString();
			var dataInputada = this.getView().byId("DT").getValue();

			if(dataInputada.length == 0){
				estado = "Error"
				erroDeValidacao = true;
				
			}else{
				var dataInputadaFormatada = new Date(dataInputada).toISOString();
			}			
			try {
				oBinding.getType().validateValue(input.getValue());
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
			input.setValueState(estado);
			return input;
		},
	});
});