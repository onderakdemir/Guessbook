
/*************************Notification****************************/

function notification(status, title, message, timeOut = 3000) {
    toastr.error(`${message}`,
        title,
        {

            // tap to dismiss
            tapToDismiss: true,

            // toast class
            toastClass: 'toast',

            // container ID
            containerId: 'toast-container',

            // debug mode
            debug: false,

            // fadeIn, slideDown, and show are built into jQuery
            showMethod: 'fadeIn',

            // duration of animation
            showDuration: 300,

            // easing function
            showEasing: 'swing',

            // callback function
            onShown: undefined,
            onHidden: undefined,

            // hide animation
            hideMethod: 'fadeOut',

            // duration of animation
            hideDuration: 500,

            // easing function
            hideEasing: 'linear',

            // close animation
            closeMethod: false,

            // duration of animation
            closeDuration: false,

            // easing function
            closeEasing: false,

            // timeout in ms
            extendedTimeOut: 1000,

            // you can customize icons here
            iconClasses: {
                error: 'toast-error',
                info: 'toast-info',
                success: 'toast-success',
                warning: 'toast-warning'
            },
            iconClass: `toast-${status}`,

            // toast-top-center, toast-bottom-center, toast-top-full-width
            // toast-bottom-full-width, toast-top-left, toast-bottom-right
            // toast-bottom-left, toast-top-right
            positionClass: 'toast-top-right',

            // set timeOut and extendedTimeOut to 0 to make it sticky
            timeOut: timeOut,

            // title class
            titleClass: `toast-${status}`,

            // message class
            messageClass: `toast-${status}`,

            // allows HTML content in the toast?
            escapeHtml: false,

            // target container
            target: 'body',

            // close button
            closeHtml: '<button type="button">&times;</button>',
            closeButton: true,

            // place the newest toast on the top
            newestOnTop: true,

            // revent duplicate toasts
            preventDuplicates: false,

            // shows progress bar
            progressBar: true

        });
    //// Without using animation
    //toastr.remove()

    //// Using animation
    //toastr.clear()
}



/**************************************************************/
