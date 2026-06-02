//contact page - add another item button
let addItem = document.querySelector(".newItem");
function addNewItem() {
    //confirm last item is completed before adding a new one
    let description = document.getElementsByName("itemDesc");
    let colour = document.getElementsByName("colour");
    let existingError = document.getElementById("itemError");
    let hasEmptyItem = false;
    for (let i = 0; i < description.length; i++) {
        if (description[i].value.trim() === "" || colour[i].value.trim() === "") {
            hasEmptyItem = true;
            if (existingError === null) {
                let errorMessage = document.createElement("p");
                errorMessage.id = "itemError";
                errorMessage.textContent = "Please complete the current item before adding a new one!";
                let itemContainer = document.getElementById("itemContainer");
                itemContainer.appendChild(errorMessage);
            }
            return;
        }
    }
    //delete any existing errors
    if (existingError !== null) {
        existingError.remove();
    }
    //adding a new item
    let itemContainer = document.getElementById("itemContainer");
    let newItem = document.createElement("div");
    newItem.classList.add("item");
    newItem.innerHTML =
    `
        <div class="form-group">
            <label>Item Description:</label>
            <input type="text" name="itemDesc" placeholder="Bandana" required />
        </div>
        <div class="form-group">
            <label>Colour(s):</label>
            <input type="text" name="colour" placeholder="Fushia" required />
        </div>
		<div class="form-group">
			<label>Reference Photo:</label>
			<input type="file" name="itemPhoto" accept="image/*" />
		</div>
    `;
    itemContainer.appendChild(newItem);
}
addItem.addEventListener("click", addNewItem);


//contact page - send button
let form = document.querySelector(".contactForm");
function submitForm(event) {
    //confirm all items are completed before sending
    let description = document.getElementsByName("itemDesc");
    let colour = document.getElementsByName("colour");
    let existingError = document.getElementById("itemError");
    let hasEmptyItem = false;
    for (let i = 0; i < description.length; i++) {
        if (description[i].value.trim() === "" || colour[i].value.trim() === "") {
            hasEmptyItem = true;
            break;
        }
    }
    if (hasEmptyItem) {
        event.preventDefault();
        if (existingError === null) {
            let errorMessage = document.createElement("p");
            errorMessage.id = "itemError";
            errorMessage.textContent = "Please complete the current item before sending the form!";
            let itemContainer = document.getElementById("itemContainer");
            itemContainer.appendChild(errorMessage);
        }
        return;
    }
    //delete any existing errors
    if (existingError !== null) {
        existingError.remove();
    }
    //allow form to submit normally if everything is valid
}
form.addEventListener("submit", submitForm);