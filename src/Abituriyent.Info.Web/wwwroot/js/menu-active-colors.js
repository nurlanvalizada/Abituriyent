$(document)
    .ready(function () {
        $(".navbar-nav>li>a")
            .click(function () {
                var id = $(this).attr("id");
                $('#' + id).siblings().find(".active").removeClass("active");
                $('#' + id).addClass("active");
                localStorage.setItem("selectedolditem", id);
            });

        var selectedolditem = localStorage.getItem('selectedolditem');

        if (selectedolditem != null && window.location.pathname.toLowerCase().indexOf(selectedolditem) > 0) {
            $('#' + selectedolditem).siblings().find(".active").removeClass("active");
            $('#' + selectedolditem).addClass("active");
        } else {
            var currentPath = window.location.pathname.toLowerCase();
            var baseMenu;
            if (currentPath.length > 2) {
                if (currentPath.indexOf('/', 2) > 0) {
                    baseMenu = currentPath.substring(1, currentPath.indexOf('/', 2));
                } else {
                    baseMenu = currentPath.substring(1);
                }

                if (currentPath.indexOf("home/about") > 0) {
                    baseMenu = "about";
                } else if (currentPath.indexOf("home/contact") > 0) {
                    baseMenu = "contact";
                }
            } else {
                baseMenu = "home";
            }
            $('#' + baseMenu.toLowerCase()).siblings().find(".active").removeClass("active");
            $('#' + baseMenu.toLowerCase()).addClass("active");
        }
    });