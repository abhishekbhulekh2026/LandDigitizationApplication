/******************************************************************************
 * Validation Library
 ******************************************************************************/

// const Validator = {

//     required(value) {

//     },

//     number(value) {

//     },

//     decimal(value) {

//     },

//     maxLength(value, length) {

//     },

//     minLength(value, length) {

//     },

//     email(value) {

//     },

//     mobile(value) {

//     }

// };

const Validator = {

    required(value) {

        return value !== null &&
            value !== undefined &&
            value !== "";

    },

    decimal(value) {

        return !isNaN(value);

    },

    integer(value) {

        return Number.isInteger(Number(value));

    },

    maxLength(value, length) {

        return value.length <= length;

    }

};