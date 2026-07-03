/******************************************************************************
 * Toolbar Component
 ******************************************************************************/

// const Toolbar = {

//     initialize() {

//     },

//     previous() {

//     },

//     next() {

//     },

//     save() {

//     },

//     saveAndNext() {

//     },

//     zoomIn() {

//     },

//     zoomOut() {

//     },

//     rotate() {

//     }

// };

const Toolbar = {

    init() {

        this.bindEvents();

    },

    bindEvents() {

        $("#btnSave").on("click", () => {

            $(document).trigger("toolbar:save");

        });

        $("#btnSaveNext").on("click", () => {

            $(document).trigger("toolbar:savenext");

        });

        $("#btnRefresh").on("click", () => {

            location.reload();

        });

    }

};