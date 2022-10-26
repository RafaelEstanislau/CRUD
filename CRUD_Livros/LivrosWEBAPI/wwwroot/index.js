sap.ui.require([
	"sap/ui/core/ComponentContainer"
], function (ComponentContainer) {
    "use strict";
    new ComponentContainer({
		name: "sap.ui.demo.walkthrough",
		settings : {
			id : "walkthrough"
		},
		async: true
	}).placeAt("content");
});

        /*sap.ui.getCore().attachInit(function () {
        var oResourceModel = new ResourceModel({

            bundleName: "sap.ui.demo.walkthrough.i18n.i18n",
            supportedLocales: [""],
            fallbackLocale: ""
        });
        sap.ui.getCore().setModel(oResourceModel, "i18n");
        var oView = new XMLView({
            viewName:"sap.ui.demo.walkthrough.view.App"
        });
        sap.ui.getCore().getMessageManager().registerObject(oView, true);
        oView.placeAt("content");

    });*/
/*fetch("https://localhost:7012/livros")
        .then(response => response.json())
        .then(data => console.log(data))*/
