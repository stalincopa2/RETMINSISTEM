    function fileValidation(input) {
        var fileInput = document.getElementById('FOTO_ARTICULO');
    var filePath = fileInput.value;
    var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
    if (!allowedExtensions.exec(filePath)) {
        alert('Solo se admiten imagenes en formato .jpg .jpeg y .png');
    fileInput.value = '';
    return false;
        } else {
            //Image preview
            if (input.files && input.files[0]) {
                var leer = new FileReader();
                leer.onload = function (e) {
                document.getElementsByTagName("img")[0].setAttribute("src", e.target.result);
                }
                leer.readAsDataURL(input.files[0]);
            }
        }
    }
