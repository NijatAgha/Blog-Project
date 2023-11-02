$(document).ready(function () {
    $('#articlesTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
        ],
        language: {
            "sDecimal": ",",
            "sEmptyTable": "Cədvəldə məlumat mövcud deyil",
            "sInfo": "_TOTAL_ məlumatdan _START_ - _END_ arasındaki məlumatlar göstərilir",
            "sInfoEmpty": "Məlumat yoxdur",
            "sInfoFiltered": "(_MAX_ məlumat içərisindən tapılan)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Səhifədə _MENU_ məlumat göstər",
            "sLoadingRecords": "Yüklənir...",
            "sProcessing": "İcra olunur...",
            "sSearch": "Axtar:",
            "sZeroRecords": "Uyğun olan məlumat tapılmadı",
            "oPaginate": {
                "sFirst": "İlk",
                "sLast": "Son",
                "sNext": "Sonrakı",
                "sPrevious": "Əvvəlki"
            },
            "oAria": {
                "sSortAscending": ": artan sütun sıralamasını aktivləşdir",
                "sSortDescending": ": azalan sütun sıralamasını aktivləşdir"
            },
            "select": {
                "rows": {
                    "_": "%d məlumat seçildi",
                    "0": "",
                    "1": "1 məlumat seçildi"
                }
            }
        }
    });
});