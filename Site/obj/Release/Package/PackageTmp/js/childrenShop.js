$(function () {
    $('.buyer').on('click', function () {
        var prod_id = $(this).data('prod_id');
        console.log(prod_id);
        $.post(
          "/AddProductToCart",
          {
              id: prod_id
          },
          "html"
        )
        .complete(function () {
            $('#cartCounter').text("Товаров в корзине: " + (parseInt($('#cartCounter').text().match(/\d+/)) + 1));
        });
        return false;
    });

    $('.buyer').on('click', function () {
        $('#cartCapacity').text("Моя корзина (" + (parseInt($('#cartCapacity').text().match(/\d+/)[0]) + 1) + ")");
        $("#productInCart").dialog({ resizable: false, minWidth: 570, minHeight: 260, show: { effect: "fadeIn", duration: 800 } });
        return false;
    });

    $('.buySect').on('click', function () {
        $('#cartCapacity').text("Моя корзина (" + (parseInt($('#cartCapacity').text().match(/\d+/)[0]) + 1) + ")");
        $("#productInCart").dialog({ resizable: false, minWidth: 570, minHeight: 260, show: { effect: "fadeIn", duration: 800 } });
        return false;
    });

    $('.productDeleter').on('click', function () {
        $("#delProdInCart").dialog({
            resizable: true, minWidth: 575, minHeight: 260, show: { effect: "fadeIn", duration: 800 }, modal: true
        });
        console.log(this);
        console.log($(this).data('prod'));
        $('#cartDelProdOK').data("prodId", $(this).data('prod'));
        console.log($('#cartDelProdOK').data("prodId"));
    });

    $('#cartDelProdOK').on('click', function () {
        var prod_id = $('#cartDelProdOK').data("prodId");
        $("#delProdInCart").dialog("close");
        $.post(
          "/DelProductFromCart",
          {
              id: prod_id
          },
          "html"
        )
        .complete(function () {
            $('.product-' + prod_id).fadeOut("slow", function () { });
        });

    });
    $('#cartDelProdNON').on('click', function () {
        $("#delProdInCart").dialog("close");
    });
})
