/******************************************************************************
 * Editable Grid Component
 ******************************************************************************/

// const EditableGrid = {

//     initialize(options) {

//     },

//     create(columns) {

//     },

//     addRow() {

//     },

//     deleteRow(index) {

//     },

//     clear() {

//     },

//     getData() {

//     },

//     setData(data) {

//     },

//     validate() {

//     },

//     save() {

//     }

// };

const EditableGrid = {

    tableSelector: "#entryGrid",

    init() {

        this.bindKeyboard();

    },

    bindKeyboard() {

        $(document).on("keydown",
            `${this.tableSelector} input`,
            function (e) {

                const current = $(this);

                if (e.key === "Enter") {

                    e.preventDefault();

                    const row = current.closest("tr");

                    const next =
                        row.next()
                            .find("input:first");

                    if (next.length) {

                        next.focus();

                    }

                }

            });

    },

    addRow() {

        console.log("Add Row");

    },

    deleteRow() {

        console.log("Delete Row");

    },

    getData() {

        const data = [];

        return data;

    },

    clear() {

    }

};