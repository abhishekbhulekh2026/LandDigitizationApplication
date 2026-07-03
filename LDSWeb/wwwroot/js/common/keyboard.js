/******************************************************************************
 * Keyboard Manager
 ******************************************************************************/

// const Keyboard = {

//     initialize() {

//     },

//     registerShortcuts() {

//     },

//     unregister() {

//     }

// };

const Keyboard = {

    init() {

        $(document).keydown(function (e) {

            if (e.ctrlKey && e.key === "s") {

                e.preventDefault();

                $(document).trigger("toolbar:save");

            }

            if (e.ctrlKey && e.key === "Enter") {

                e.preventDefault();

                $(document).trigger("toolbar:savenext");

            }

            if (e.key === "F3") {

                e.preventDefault();

                $("#txtSearch").focus();

            }

        });

    }

};