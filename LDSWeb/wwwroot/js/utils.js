const Utils = {

    isEmpty(value) {

        return value == null ||

            value.trim() == "";

    },

    toDecimal(value) {

        return parseFloat(value) || 0;

    },

    toInt(value) {

        return parseInt(value) || 0;

    }

};