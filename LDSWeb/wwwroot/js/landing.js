// =========================================================
// Landing Page
// Lucknow Bhulekh Digitization Portal
// =========================================================

"use strict";

const Landing = {

    init() {

        this.bindEvents();
        this.loadStatistics();
        this.startCounters();
        this.startNoticeBoard();
    },

    bindEvents() {

        // Language Change
        $(".language-select").on("change", function () {

            const lang = $(this).val();

            console.log("Language :", lang);

            // Future:
            // Localization.changeLanguage(lang);

        });

        // Officer Login
        $(".btn-login").on("click", function () {

            window.location.href = "/Login";

        });

    },

    //==============================================
    // Temporary Statistics
    //==============================================

    loadStatistics() {

        // Later these values will come from API

        $("#districtCount").text("01");
        $("#villageCount").text("674");
        $("#volumeCount").text("16,271");
        $("#pageCount").text("13,91,684");

    },

    //==============================================
    // Future Counter Animation
    //==============================================

    startCounters() {

        $(".stat-card").hover(function () {

            $(this).addClass("shadow-lg");

        }, function () {

            $(this).removeClass("shadow-lg");

        });

    },

    //==============================================
    // Notice Board
    //==============================================

    startNoticeBoard() {

        console.log("Notice Board Ready");

    }

};

//==========================================================
// Document Ready
//==========================================================

$(function () {

    Landing.init();

});