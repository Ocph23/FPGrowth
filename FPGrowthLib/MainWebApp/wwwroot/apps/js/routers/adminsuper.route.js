angular.module("adminsuper.router", [])
    .config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/account/login');
        $stateProvider
            .state("adminsuper", {
                url: '/adminsuper',
                templateUrl: 'apps/views/adminsuper/index.html'
            })
            .state("adminsuper-home", {
                url: '/home',
                parent: "adminsuper",
                controller: 'adminsuperHomeController',
                templateUrl: 'apps/views/adminsuper/home.html'
            })
            .state("adminsuper-menuutama", {
                url: '/menuutama',
                parent: "adminsuper",
                controller: 'adminsuperMenuUtamaController',
                templateUrl: 'apps/views/adminsuper/menuutama.html'
            })
            .state("adminsuper-daftarkategori", {
                url: '/daftarkategori',
                parent: "adminsuper",
                controller: 'adminsuperDaftarKategoriController',
                templateUrl: 'apps/views/adminsuper/DaftarKategori.html'
            })
            .state("adminsuper-tambahkategori", {
                url: '/tambahkategori',
                parent: "adminsuper",
                controller: 'adminsuperTambahKategoriController',
                templateUrl: 'apps/views/adminsuper/tambahkategori.html'
            })
            .state("adminsuper-dataparameter", {
                url: '/dataparameter',
                parent: "adminsuper",
                controller: 'adminsuperDataParameterController',
                templateUrl: 'apps/views/adminsuper/dataparameter.html'
            })
            .state("adminsuper-datatransaksi", {
                url: '/datatransaksi',
                parent: "adminsuper",
                controller: 'adminsuperDataTransaksiController',
                templateUrl: 'apps/views/adminsuper/datatransaksi.html'
            })
            .state("adminsuper-analisa", {
                url: '/analisa',
                parent: "adminsuper",
                controller: 'adminsuperAnalisaController',
                templateUrl: 'apps/views/adminsuper/analisa.html'
            })
            .state("adminsuper-managementransaksi", {
                url: '/managementransaksi',
                parent: "adminsuper",
                controller: 'adminsuperManagemenTransaksiController',
                templateUrl: 'apps/views/adminsuper/managementransaksi.html'
            })
            .state("adminsuper-datapenjual", {
                url: '/datapenjual',
                parent: "adminsuper",
                controller: 'adminsuperDataPenjualController',
                templateUrl: 'apps/views/adminsuper/datapenjual.html'
            })
            .state("adminsuper-datapembeli", {
                url: '/datapembeli',
                parent: "adminsuper",
                controller: 'adminsuperDataPembeliController',
                templateUrl: 'apps/views/adminsuper/datapembeli.html'
            })
            .state("adminsuper-dataorder", {
                url: '/dataorder',
                parent: "adminsuper",
                controller: 'adminsuperDataOrderController',
                templateUrl: 'apps/views/adminsuper/dataorder.html'
            })
            .state("adminsuper-konfirpembayaran", {
                url: '/konfirpembayaran',
                parent: "adminsuper",
                controller: 'adminsuperKonfirPembayaranController',
                templateUrl: 'apps/views/adminsuper/konfirpembayaran.html'
            })
            .state("adminsuper-konfirpengiriman", {
                url: '/konfirpengiriman',
                parent: "adminsuper",
                controller: 'adminsuperKonfirPengirimanController',
                templateUrl: 'apps/views/adminsuper/konfirpengiriman.html'
            })
    });