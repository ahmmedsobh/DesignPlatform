// Start Slider 

var swiper = new Swiper(".mySwiper", {
    slidesPerView: 3,
    spaceBetween: 30,
    loop: true,
    pagination: {
      el: ".swiper-pagination",
      clickable: true,
    },
    
    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },
  });

// End Slider 

// Start Packages
    var btn = document.getElementsByClassName("view-details-btn");
    var i;
    
    for (i = 0; i < btn.length; i++) {
      btn[i].addEventListener("click", function() {
        //this.classList.toggle("active");
        var content = this.nextElementSibling;
        content.style.display = "block";
        this.style.display = "none";
      });
    }

// End Packages


// Start Select Packages
let modalEle = document.getElementById("select-package-modal");
if(modalEle != undefined && modalEle != null)
{
  let modal = new bootstrap.Modal(modalEle); 

  
     modal.show();
}



// End Select Packages


// Start Reviews 

var swiper = new Swiper(".ReviewsSwiper", {
  slidesPerView: 2,
  spaceBetween: 30,
  loop: true,
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
  },
});

// End Reviews 

// Start Add Appointment

$('#DatePicker').datetimepicker({
	inline:true,
  formatTime:'g:i',
	formatDate:'d.m.Y',
  allowTimes: function getArr() {
    var allowTimes = [
       '12:00', '12:30', '13:00', '13:30', '14:00', 
       '14:30', '15:00', '15:30', '19:00', '20:00'
    ];
return allowTimes;
}()
});

// Start Add Appointment


// Start Project Details
let input1 = document.getElementsByClassName("upload-img1-file");

if (input1[0] != undefined) {
    input1[0].addEventListener("change", () => {




        const files = input1[0].files;

        var imgs = ``;


        for (var i = 0; i < files.length; i++) {
            var src = URL.createObjectURL(files[i]);
            imgs += `
          <div class="col-lg-4">
            <div class="img">
                <img class="img-item" src="${src}">
                <button type="button" class="img-delete-btn img1-delete-btn" data-file-index="${i}">
                    <i class="fa-solid fa-xmark"></i>
                </button>
            </div>
         </div>
      `;
        }

        console.log(imgs)

        $(".images1-row").append(imgs);


        // if (file) {
        //   imageView[0].src = URL.createObjectURL(file);
        //   imgName[0].innerText = file.name;

        //   imgViewContainer[0].style.display = 'flex';
        // }

    });
}


$(document).on("click", ".img1-delete-btn", function () {

    $(this).closest(".col-lg-4").remove();

    var index = $(this).attr("data-file-index");

    var dt = new DataTransfer();

    for (var i = 0; i < $(".upload-img1-file")[0].files.length; i++) {
        if (i != index) {
            dt.items.add($(".upload-img1-file")[0].files[i])
        }
    }

    $(".upload-img1-file")[0].files = dt.files;

})

////////////////

let input2 = document.getElementsByClassName("upload-img2-file");

if (input2[0] != undefined) {
    input2[0].addEventListener("change", () => {




        const files = input2[0].files;

        var imgs = ``;


        for (var i = 0; i < files.length; i++) {
            var src = URL.createObjectURL(files[i]);
            imgs += `
          <div class="col-lg-4">
            <div class="img">
                <img class="img-item" src="${src}">
                <button type="button" class="img-delete-btn img2-delete-btn" data-file-index="${i}">
                    <i class="fa-solid fa-xmark"></i>
                </button>
            </div>
         </div>
      `;
        }

        $(".images2-row").append(imgs);


        // if (file) {
        //   imageView[0].src = URL.createObjectURL(file);
        //   imgName[0].innerText = file.name;

        //   imgViewContainer[0].style.display = 'flex';
        // }

    });
}


$(document).on("click", ".img2-delete-btn", function () {

    $(this).closest(".col-lg-4").remove();

    var index = $(this).attr("data-file-index");

    var dt = new DataTransfer();

    for (var i = 0; i < $(".upload-img2-file")[0].files.length; i++) {
        if (i != index) {
            dt.items.add($(".upload-img2-file")[0].files[i])
        }
    }

    $(".upload-img2-file")[0].files = dt.files;

})

////////////////

let input3 = document.getElementsByClassName("upload-img3-file");

if (input3[0] != undefined) {
    input3[0].addEventListener("change", () => {




        const files = input3[0].files;

        var imgs = ``;


        for (var i = 0; i < files.length; i++) {
            var src = URL.createObjectURL(files[i]);
            imgs += `
          <div class="col-lg-4">
            <div class="img">
                <img class="img-item" src="${src}">
                <button type="button" class="img-delete-btn img3-delete-btn" data-file-index="${i}">
                    <i class="fa-solid fa-xmark"></i>
                </button>
            </div>
         </div>
      `;
        }

        $(".images3-row .col-lg-4").remove()
        $(".images3-row").append(imgs);


        // if (file) {
        //   imageView[0].src = URL.createObjectURL(file);
        //   imgName[0].innerText = file.name;

        //   imgViewContainer[0].style.display = 'flex';
        // }

    });
}


$(document).on("click", ".img3-delete-btn", function () {

    $(this).closest(".col-lg-4").remove();

    var index = $(this).attr("data-file-index");

    var dt = new DataTransfer();

    for (var i = 0; i < $(".upload-img3-file")[0].files.length; i++) {
        if (i != index) {
            dt.items.add($(".upload-img3-file")[0].files[i])
        }
    }

    $(".upload-img3-file")[0].files = dt.files;

})

// End Project Details


