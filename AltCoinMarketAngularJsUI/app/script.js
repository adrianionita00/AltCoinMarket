angular.module('myapp', ['myapp.controller']);

angular.module('myapp.controller', ['myapp.service'])
    .controller('testController', function ($scope, testService) {

        $scope.posts = {};
        function GetAllPosts() {
            var getPostsData = testService.getPosts();

            getPostsData.then(function (post) {
                $scope.posts = post.data;

            }, function () {
                alert('Error in getting post records');
            });
        }

        GetAllPosts();
    });

angular.module('myapp.service', [])
    .service('testService', function ($http) {
        
        this.getPosts = function () {            
            return $http.get('http://localhost:52974/api/ticker');
        };
    });