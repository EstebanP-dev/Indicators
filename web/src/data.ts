export const menu = [
    {
        id: 1,
        title: "Principal",
        listItems: [
            {
                id: 1,
                title: "Inicio",
                url: "/",
                icon: "home.svg",
                roles: [

                ]
            },
            {
                id: 2,
                title: "Mi Perfil",
                url: "/",
                icon: "user.svg",
                roles: [
                    1,
                    2,
                    3,
                    4,
                    5
                ]
            },
        ]
    },
    {
        id: 2,
        title: "Gestión",
        listItems: [
            // {
            //     id: 1,
            //     title: "Usuarios",
            //     url: "/",
            //     icon: "user.svg"
            // },
            // {
            //     id: 2,
            //     title: "Roles",
            //     url: "/",
            //     icon: "user-gear.svg"
            // },
            {
                id: 3,
                title: "Visualizaciones",
                url: "/displays",
                icon: "user-gear.svg",
                roles: [
                    1,
                    2,
                    3,
                    4,
                    5
                ]
            },
        ]
    },
    {
        id: 3,
        title: "Mantenimiento",
        listItems: [
            {
                id: 1,
                title: "Settings",
                url: "/",
                icon: "settings.svg",
                roles: [
                    1,
                    2,
                    3,
                    4,
                    5
                ]
            },
        ]
    },
]