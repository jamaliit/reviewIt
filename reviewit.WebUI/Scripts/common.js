//$(document).ready(function () {

//    $('#create, .edit').on('click', function (e) {
//        e.preventDefault();

//        showModal($(this).attr('href'), $(this).data('title'), '#commonModal');
//    })

//    var showModal = function (url, title, modal_container) {
//        $.get(url, function (res) {
//            $('.modal-body').html(res);
//            $('.modal-title').html(title);
//            $(modal_container).moadl();
//        })
//    }
//});

//function success(data) {
//    window.location.reload(true);
//}

//function confirmBox(href) {
//    bootbox.confirm({
//        className: 'my-modal',
//        size: 'small',
//        message: "Are you sure?",
//        callback: function (result) {
//            if (result) {
//                window.location = href;
//                return true;
//            }
//        }
//    })

//    return false;
//}
