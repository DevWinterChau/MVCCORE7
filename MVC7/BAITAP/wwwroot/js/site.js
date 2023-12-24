// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Function to get cart items from local storage
function Total(cartItems) {
    var total = 0;
    cartItems.forEach(function (item) {
        total += (item.giaban * item.soluongmua);
    });
    return total;
}
// Hàm kiểm tra và vô hiệu hóa nút
function checkCartLength() {
    var cartItems = getCartItems();
    var paymentButton1 = document.getElementById('btn-payment1');  // Assuming the ID is 'btn-payment'
    var paymentButton2 = document.getElementById('btn-payment2');  // Assuming the ID is 'btn-payment'
    var paymentButton3 = document.getElementById('btn-payment3');  // Assuming the ID is 'btn-payment'

    // Kiểm tra chiều dài của cart
    if (cartItems.length === 0) {
        // Nếu cart rỗng, vô hiệu hóa nút
        paymentButton1.disabled = true;
        paymentButton2.disabled = true;
        paymentButton3.disabled = true;

    } else {
        // Nếu cart có sản phẩm, bỏ vô hiệu hóa nút (nếu chúng đã bị vô hiệu hóa)
        paymentButton1.disabled = false;
        paymentButton2.disabled = false;
        paymentButton3.disabled = false;
    }
}

function formatCurrency(amount) {
    return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(amount);
}

// Function to display cart items
function displayCartItems() {
    var cartItems = getCartItems();
    var cartContainer = document.getElementById('cart-container');
    var itemCountElement = document.getElementById('cart-item-count');
    var carttotalproduct = document.getElementById('cart-total-product');
    var carttotal = document.getElementById('cart-total');

    // Clear existing content
    cartContainer.innerHTML = '';

    // Update the item count
    itemCountElement.textContent = cartItems.length;

    carttotal.textContent = formatCurrency(Total(cartItems));
    carttotalproduct.textContent = formatCurrency(Total(cartItems));


    // Iterate through each item in the cart and generate HTML
    cartItems.forEach(function (item) {
        var productHtml = `
                    <div class="row">
                            <div class="col-lg-3 col-md-12 mb-4 mb-lg-0">
                                        <!-- Image -->
                                        <div class="bg-image hover-overlay hover-zoom ripple rounded" data-mdb-ripple-color="light">
                                                            <a href="#!">
                                                <div class="mask" style="background-color: rgba(251, 251, 251, 0.2)"></div>
                                            </a>
                                        </div>
                                        <!-- Image -->
                                    </div>

                                    <div class="col-lg-5 col-md-6 mb-4 mb-lg-0">
                                        <!-- Data -->
                                        <p><strong>${item.ten}</strong></p>
                                            <button type="button" class="btn btn-primary btn-sm me-1 mb-2" data-mdb-toggle="tooltip" title="Xóa"  onclick="removeItem('${item.id}')">
                                            <i class="bi bi-x-square-fill"></i>
                                        </button>
                                            <button type="button" class="btn btn-danger btn-sm mb-2" data-mdb-toggle="tooltip" title="Chuyển vào danh sách yêu thích"  onclick="removeItem('${item.id}')">
                                            <i class="bi bi-bag-heart-fill"></i>
                                        </button>
                                        <!-- Data -->
                                    </div>

                                    <div class="col-lg-4 col-md-6 mb-4 mb-lg-0">
                                        <!-- Quantity -->
                                        <div class="d-flex mb-4" style="max-width: 300px">
                                                    <button class="btn btn-primary px-3 me-2" onclick="decreaseQuantity('${item.id}')">
                                                <i class="bi bi-dash-square-fill"></i>
                                            </button>

                                            <div class="form-outline">
                                                        <input
                                                            id="quantity-${item.id}"
                                                            min="0"
                                                            name="quantity"
                                                            value="${item.soluongmua}"
                                                            type="number"
                                                            class="form-control"
                                                            onChange="updateQuantity('${item.id}')"
                                                        />
                                                        <label class="form-label" for="quantity-${item.id}">Số lượng</label>
                                            </div>

                                                <button class="btn btn-primary px-3 ms-2" onclick="updateQuantity('${item.id}')">
                                                <i class="bi bi-plus-square-fill"></i>
                                            </button>
                                        </div>
                                        <!-- Quantity -->

                                        <!-- Price -->
                                        <p class="text-start text-md-center">
                                            <strong>${item.giaban}</strong>
                                        </p>
                                        <!-- Price -->
                                    </div>
                    </div>
                    <hr class="my-4" />
                `;

        // Append the generated HTML to the cart container
        cartContainer.innerHTML += productHtml;
    });
    checkCartLength();
}
// Function to update quantity in local storage
function updateQuantity(productId) {
    var cartItems = getCartItems();

    // Find the item in the cart
    var item = cartItems.find(i => i.id === productId);
    console.log(item);
    if (item) {
        // Update the quantity and save back to local storage
        item.soluongmua++;
        localStorage.setItem('cart', JSON.stringify(cartItems));

        // Update the displayed cart items
        displayCartItems();
    }
}

// Function to update quantity in local storage
// Function to update quantity in local storage
function decreaseQuantity(productId) {
    var cartItems = getCartItems();

    // Find the index of the item in the cart
    var index = cartItems.findIndex(i => i.id === productId);

    if (index !== -1) {
        // Update the quantity
        cartItems[index].soluongmua--;

        // If the quantity becomes 0, remove the item from the cart
        if (cartItems[index].soluongmua === 0) {
            cartItems.splice(index, 1);
        } else {
            // Save the modified cartItems back to local storage
            localStorage.setItem('cart', JSON.stringify(cartItems));
        }

        // Update the displayed cart items
        displayCartItems();
    }
}


function removeItem(productId) {
    var cartItems = getCartItems();

    // Find the index of the item in the cart
    var index = cartItems.findIndex(i => i.id === productId);

    if (index !== -1) {
        // Remove the item at the found index
        cartItems.splice(index, 1);

        // Update the localStorage with the modified cartItems
        localStorage.setItem('cart', JSON.stringify(cartItems));

        // Update the displayed cart items
        displayCartItems();
    }
}


function totalPayment() {

    // Rest of your code remains unchanged
    var cartItems = getCartItems();
    if (cartItems.length > 0) {
        var dataToSend = {
            cartItems: cartItems,
        };

        // Sử dụng Ajax để gửi dữ liệu tới action Total
        $.ajax({
            url: '/customer/total',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dataToSend),
            success: function (response) {
                if (response.success) {
                    window.location.href = '/';
                }
                else {
                    var currentUrl = window.location.href;
                    window.location.href = '/customer/Login?returnUrl=' + encodeURIComponent(currentUrl);
                }
            },
            error: function (error) {
                console.error(error);
            }
        });
    } else {

        // Cart rỗng, xử lý tùy ý (hiển thị thông báo, chuyển hướng, ...)
    }

}

function formatCurrency(amount) {
    return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(amount);
}
function getCartItems() {
    return JSON.parse(localStorage.getItem('cart')) || [];
}
function displayNumbercart() {
    var itemCountElement = document.getElementById('cart-item-count');
    var carts = getCartItems();
    itemCountElement.textContent = carts.length;
}
// Your JavaScript code for shopping cart functionality using local storage

/*
function addToCart(id, giaban, hinhanh, ten, giakhuyenmai) {
    console.log(id, giaban, hinhanh, ten, giakhuyenmai);  // Log the parameters directly
    var cart = JSON.parse(localStorage.getItem('cart')) || [];
    var existingProduct = cart.find(item => item.id === id);
    if (existingProduct) {
        existingProduct.soluongmua++;
        Toastify({
            text: "Đã cập nhật số lượng vào giỏ hàng !",
            duration: 3000,
            gravity: "bottom", // Chọn bottom để hiển thị ở dưới
            position: 'right',
            backgroundColor: "rgba(0, 128, 0, 0.8)", // Màu nền xanh lá cây với độ trong suốt
            stopOnFocus: true,
            className: 'custom-toast', // Thêm class để tùy chỉnh CSS
        }).showToast();
    } else {
        cart.push({ id, ten, giaban, hinhanh, soluongmua: 1, giakhuyenmai: giakhuyenmai });
        // Hiển thị toast thông báo
        Toastify({
            text: "Đã thêm vào giỏ hàng !",
            duration: 3000,
            gravity: "bottom",
            position: 'right',
            backgroundColor: "rgba(0, 128, 0, 0.8)", // Màu nền xanh lá cây với độ trong suốt
            stopOnFocus: true,
            className: 'custom-toast', // Thêm class để tùy chỉnh CSS
        }).showToast();

    }

    console.log(cart);
    localStorage.setItem('cart', JSON.stringify(cart));
    displayNumbercart();
}
*/
function removeFromCart(productId) {
    var cart = JSON.parse(localStorage.getItem('cart')) || [];
    cart = cart.filter(item => item.id !== productId);
    localStorage.setItem('cart', JSON.stringify(cart));
}

function removeAllCart() {
    localStorage.setItem('cart', JSON.stringify(null));
    displayCartItems();
    displayNumbercart();
}


