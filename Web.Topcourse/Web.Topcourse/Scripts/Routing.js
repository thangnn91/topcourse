var isLoadPage = true;
var Routing = function (appRoot, contentSelector, defaultRoute) {

    function getUrlFromHash(hash) {
        var url = hash.replace('#/', '');
        if (url === appRoot)
            url = defaultRoute;
        return url;
    }
    return {
        init: function () {
            Sammy(contentSelector, function () {
                //debugger;
                //this.get('/', function (context) {
                //    // context is equalient to data.app in the custom bind example
                //    // currentComponent('home'); I use components in my code but you should be able to swith to your implementation
                //    var url = getUrlFromHash(context.path);
                //    context.load(url).swap();
                //});
                this.get(/(.*)/, function (context) {
                    if (isLoadPage) {
                        isLoadPage = false;
                        return;
                    }
                    ///debugger;
                    var url = getUrlFromHash(context.path);                       
                    context.load(url).swap();
                });

            }).run();
        }
    };
}


//var app = Sammy(function (appRoot, contentSelector, defaultRoute) {

//    this.get(/\#\/(.*)/, function (context) {
//        // context is equalient to data.app in the custom bind example
//        // currentComponent('home'); I use components in my code but you should be able to swith to your implementation
//        var url = getUrlFromHash(context.path);
//        context.load(url).swap();
//    });

//    this.bind('mycustom-trigger', function (e, data) {
//        this.redirect('/'); // force redirect
//    });

//    this.get('/about', function (evt) {
//        // currentComponent('about'); I use components in my code but you should be able to swith to your implementation
//        var url = getUrlFromHash(context.path);
//        context.load(url).swap();
//    });
//    function getUrlFromHash(hash) {
//        var url = hash.replace('#/', '');
//        if (url === appRoot)
//            url = defaultRoute;
//        return url;
//    }
//});
