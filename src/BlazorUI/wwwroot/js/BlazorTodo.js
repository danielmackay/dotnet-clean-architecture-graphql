var blazorTodo = {};

blazorTodo.setStorage = function (key, data) {
    localStorage.setItem(key, data);
}

blazorTodo.getStorage = function (key) {
    return localStorage.getItem(key);
}

//blazorTodo.hideModal = function (element) {
//    bootstrap.Modal.getInstance(element).hide();
//}