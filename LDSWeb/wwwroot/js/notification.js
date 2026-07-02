const Notify = {

    success(msg) {

        Swal.fire({

            icon: "success",

            text: msg

        });

    },

    error(msg) {

        Swal.fire({

            icon: "error",

            text: msg

        });

    },

    warning(msg) {

        Swal.fire({

            icon: "warning",

            text: msg

        });

    }

};