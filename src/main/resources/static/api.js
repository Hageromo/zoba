function createNode(element) {
    return document.createElement(element);
}
function append(parent, el) {
    return parent.appendChild(el);
}

const ul = document.getElementById('users');

fetch("api/users")
.then((resp) => resp.json())
.then(function(data) {
    let users = data;
    return users.map(function(user) {
        let li = createNode('li'),
            span = createNode('span');
        li.innerHTML = `${user.id} ${user.email}`;
        append(li, span);
        append(ul, li);
    })
})
