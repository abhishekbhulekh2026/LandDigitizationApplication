$(function () {

    initializePage();

});

function initializePage() {

    Page.init();

    Toolbar.init();

    Keyboard.init();

    PdfViewer.init();

    EditableGrid.init();

    StatusBar.setStatus("Ready");

    bindEvents();

}

function bindEvents() {

    $(document).on("toolbar:save",
        async function () {

            await saveRecord();

        });

    $(document).on("toolbar:savenext",
        async function () {

            await saveAndNext();

        });

}

async function saveRecord() {

    try {

        Loader.show();

        StatusBar.setStatus("Saving...");

        const data =
            EditableGrid.getData();

        console.log(data);

        StatusBar.setStatus("Saved");

        Notification.success(
            "Record saved successfully");

    }
    catch (e) {

        Notification.error(e.message);

    }
    finally {

        Loader.hide();

    }

}

async function saveAndNext() {

    await saveRecord();

    Notification.success(
        "Moving to next page");

}