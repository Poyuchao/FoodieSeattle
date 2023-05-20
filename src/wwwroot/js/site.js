


    // function for dynamic background
    var images = [

    'landing_page_background_photo.jpg',
    '2.jpg',
    '3.jpg',
    // add more image file names here...
    ];

    var currentImageIndex = 0;

        function changeBackgroundImage() {

            var dynamicBackground = document.getElementById('dynamic-background');

            if (dynamicBackground) {
        dynamicBackground.style.backgroundImage = "url('/img/images/" + images[currentImageIndex] + "')";
                dynamicBackground.style.backgroundSize = "cover"; // for full window background

                currentImageIndex++;
                if (currentImageIndex == images.length) { // Reset the counter to loop the images
        currentImageIndex = 0;
                }
            }

        }

            window.onload = function () {
        // Set the initial background
        changeBackgroundImage();

                // Change the background every 5 seconds
                setInterval(changeBackgroundImage, 5000);
            };




