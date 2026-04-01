window.initializeSidebar = function () {
    const body = document.querySelector("body");
    const darkLight = document.querySelector("#darkLight");
    const profileBtn = document.querySelector("#profileBtn");
    const profileMenu = document.querySelector("#profileMenu");
    const notificationBtn = document.querySelector("#notificationBtn");
    const notificationMenu = document.querySelector("#notificationMenu");
    const sidebar = document.querySelector(".sidebar");
    const Body = document.querySelector(".content-container");
    const submenuItems = document.querySelectorAll(".submenu_item");
    const sidebarOpen = document.querySelector("#sidebarOpen");
    const sidebarClose = document.querySelector(".collapse_sidebar");
    const sidebarExpand = document.querySelector(".expand_sidebar");

    sidebarOpen.addEventListener("click", () => sidebar.classList.toggle("close"));

    sidebarClose.addEventListener("click", () => {
        sidebar.classList.add("close", "hoverable");
        Body.classList.add("close", "hoverable");
    });
    sidebarExpand.addEventListener("click", () => {
        sidebar.classList.remove("close", "hoverable");
        Body.classList.remove("close", "hoverable");
    });

    sidebar.addEventListener("mouseenter", () => {
        if (sidebar.classList.contains("hoverable")) {
            sidebar.classList.remove("close");
        }
    });
    sidebar.addEventListener("mouseleave", () => {
        if (sidebar.classList.contains("hoverable")) {
            sidebar.classList.add("close");
        }
    });

    darkLight.addEventListener("click", () => {
        body.classList.toggle("dark");
        if (body.classList.contains("dark")) {
            darkLight.classList.replace("bx-sun", "bx-moon");
        } else {
            darkLight.classList.replace("bx-moon", "bx-sun");
        }
    });

    submenuItems.forEach((item, index) => {
        item.addEventListener("click", () => {
            item.classList.toggle("show_submenu");
            submenuItems.forEach((item2, index2) => {
                if (index !== index2) {
                    item2.classList.remove("show_submenu");
                }
            });
        });
    });

    if (window.innerWidth < 768) {
        sidebar.classList.add("close");
        Body.classList.add("cero");
    } else {
        sidebar.classList.remove("close");
        Body.classList.remove("cero");
    }

    profileBtn.addEventListener("click", () => {
        profileMenu.classList.toggle("active");
    });

    document.addEventListener("click", (event) => {
        if (!profileBtn.contains(event.target) && !profileMenu.contains(event.target)) {
            profileMenu.classList.remove("active");
        }
    });

    notificationBtn.addEventListener("click", () => {
        notificationMenu.classList.toggle("active");
    });

    document.addEventListener("click", (event) => {
        if (!notificationBtn.contains(event.target) && !notificationMenu.contains(event.target)) {
            notificationMenu.classList.remove("active");
        }
    });
};
