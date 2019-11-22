let polizaCounter = 0;

$(document).ready(() => {
    polizaCounter = $("#polizasList input").length;

    $("#addPolicy").click(() => {
        /*let newRow = $("#Pólizas tbody")[0].insertRow();
        let noPoliza = newRow.insertCell(0);
        let error = newRow.insertCell(1);
        agregarInput().forEach((element) => {
            $(noPoliza).append(element);
        });*/

        agregarInput().forEach((element) => {
            $("#polizasList").append(element);
        });
        

    });
});

agregarInput = () => {
    let newIndex = $(document.getElementById("input_template_Index")).clone();
    let newInput = $(document.getElementById("input_template_NumeroPoliza")).clone();

    newIndex.prop("id", "Polizas_" + polizaCounter + "__Index");
    newIndex.prop("value", polizaCounter);

    newInput.prop("id", "Polizas_" + polizaCounter + "__NumeroPoliza");
    newInput.prop("name", "Polizas[" + polizaCounter + "].NumeroPoliza");
    newInput.removeClass("hidden");

    polizaCounter++;

    return [newIndex, newInput];
}