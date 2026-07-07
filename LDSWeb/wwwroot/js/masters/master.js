$(function () {

    loadDistricts();
    loadTehsils();
    loadParganas();
});

async function loadDistricts() {

    try {

        Loader.show();

        const result = await Api.request("/api/Master/DistrictList");

        Loader.hide();

        if (!result.Status) {

            Notify.error(result.Message);
            return;
        }

        const ddl = $("#ddlDistrict");

        ddl.empty();

        ddl.append('<option value="">-- Select District --</option>');

        $.each(result.Data, function (i, item) {

            ddl.append(
                `<option value="${item.DistrictId}">
                    ${item.NameEn}
                </option>`
            );

        });

    }
    catch (e) {

        Loader.hide();

        Notify.error(e.message);

    }

}

async function loadTehsils() {

    try {

        Loader.show();

        const result = await Api.request("/api/Master/TehsilList");

        Loader.hide();

        if (!result.Status) {

            Notify.error(result.Message);
            return;
        }

        const ddl = $("#ddlTehsil");

        ddl.empty();

        ddl.append('<option value="">-- Select Tehsil --</option>');

        $.each(result.Data, function (i, item) {

            ddl.append(
                `<option value="${item.TehsilId}">
                    ${item.NameEn}
                </option>`
            );

        });

    }
    catch (e) {

        Loader.hide();

        Notify.error(e.message);

    }

}

async function loadParganas() {

    try {

        Loader.show();

        const result = await Api.request("/api/Master/ParganaList");

        Loader.hide();

        if (!result.Status) {

            Notify.error(result.Message);
            return;
        }

        const ddl = $("#ddlPargana");

        ddl.empty();

        ddl.append('<option value="">-- Select Pargana --</option>');

        $.each(result.Data, function (i, item) {

            ddl.append(
                `<option value="${item.ParganaId}">
                    ${item.NameEn}
                </option>`
            );

        });

    }
    catch (e) {

        Loader.hide();

        Notify.error(e.message);

    }

}

async function bindVillage() {

        const tehsilId = $("#ddlTehsil").val();
        const parganaId = $("#ddlPargana").val();

        const ddl = $("#ddlVillage");

        ddl.empty();
        ddl.append('<option value="">Loading...</option>');

        if (!tehsilId || !parganaId) {

            ddl.empty();
            ddl.append('<option value="">-- Select Village --</option>');
            return;
        }

        try {

            const result = await Api.request(
                `/api/Master/VillageList?tehsilId=${tehsilId}&parganaId=${parganaId}`
            );

            ddl.empty();

            ddl.append('<option value="">-- Select Village --</option>');

            if (!result.Status) {
                Notify.error(result.Message);
                return;
            }

            $.each(result.Data, function (_, item) {

                ddl.append(
                    `<option value="${item.VillageId}">
                        ${item.NameEn}
                    </option>`
                );

            });

        }
        catch (e) {

            Notify.error(e.message);

        }
}

async function bindRecordType() {

        const villageId = $("#ddlVillage").val();

        const ddl = $("#ddlRecordType");

        ddl.empty();
        ddl.append('<option value="">Loading...</option>');

        if (!villageId) {

            ddl.empty();
            ddl.append('<option value="">-- Select Record Type --</option>');
            return;
        }

        try {

            const result = await Api.request(
                `/api/Master/RecordTypeList?villageId=${villageId}`
            );

            ddl.empty();

            ddl.append('<option value="">-- Select Record Type --</option>');

            if (!result.Status) {

                Notify.error(result.Message);
                return;
            }

            $.each(result.Data, function (_, item) {

                ddl.append(
                    `<option value="${item.RecordTypeId}">
                        ${item.DisplayName}
                    </option>`
                );

            });

        }
        catch (e) {

            Notify.error(e.message);

        }

}

async function bindVolume() {

    const recordTypeId = $("#ddlRecordType").val();

    const ddl = $("#ddlVolume");

    ddl.empty();
    ddl.append('<option value="">Loading...</option>');

    if (!recordTypeId) {

        ddl.empty();
        ddl.append('<option value="">-- Select Volume --</option>');
        return;
    }

    try {

        const result = await Api.request(
            `/api/Master/RecordVolumeList?VolumeId=${recordTypeId}`
        );

        ddl.empty();

        ddl.append('<option value="">-- Select Volume --</option>');

        if (!result.Status) {

            Notify.error(result.Message);
            return;
        }

        $.each(result.Data, function (_, item) {

            ddl.append(
                `<option value="${item.VolumeId}">
                    ${item.VolumeNumber}
                </option>`
            );

        });

    }
    catch (e) {

        Notify.error(e.message);

    }

}

$(function () {

    $("#ddlTehsil").change(function () {

        bindVillage();

    });

    $("#ddlPargana").change(function () {

        bindVillage();

    });

});

$(function () {

    $("#ddlVillage").on("change", function () {

        bindRecordType();

    });

});

$("#ddlRecordType").on("change", function () {

    bindVolume();

});
