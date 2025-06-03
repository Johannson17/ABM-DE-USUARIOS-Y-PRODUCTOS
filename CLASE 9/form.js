const form = document.getElementById("suscription-form");
const greeting = document.getElementById("form-greeting");
const fields = { 
  fullName: false, email: false, password: false, confirmPassword: false,
  age: false, phone: false, address: false, city: false, postalCode: false, dni: false
};

const validators = {
  fullName: value => {
    if (!/^[a-zA-ZÀ-ÿ\s]{7,}$/.test(value)) return "Debe tener más de 6 letras y solo letras/espacios.";
    if (!/\s/.test(value)) return "Debe tener al menos un espacio (nombre y apellido).";
    return "";
  },
  email: value => /^[\w-.]+@([\w-]+\.)+[\w-]{2,}$/.test(value) ? "" : "Ingrese un email válido.",
  password: value => /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/.test(value)
    ? "" : "Mínimo 8 caracteres, letras y números.",
  confirmPassword: value => value === document.getElementById("password").value ? "" : "Las contraseñas deben coincidir.",
  age: value => Number.isInteger(+value) && +value >= 18 ? "" : "Debe ser mayor o igual a 18.",
  phone: value => /^\d{7,}$/.test(value) ? "" : "Solo números, al menos 7 dígitos, sin espacios ni símbolos.",
  address: value => /^[a-zA-Z0-9\s]{5,}$/.test(value) && /\s/.test(value)
    ? "" : "Al menos 5 caracteres, letras, números y un espacio.",
  city: value => value.length >= 3 ? "" : "Debe tener al menos 3 caracteres.",
  postalCode: value => value.length >= 3 ? "" : "Debe tener al menos 3 caracteres.",
  dni: value => /^\d{7,8}$/.test(value) ? "" : "Debe ser un número de 7 u 8 dígitos."
};

// Saludo en tiempo real
const nameInput = document.getElementById("fullName");
nameInput.addEventListener("input", e => {
  greeting.textContent = "HOLA" + (e.target.value.trim() ? " " + e.target.value.trim() : "");
});

// Validaciones en blur y mensajes en focus
form.querySelectorAll("input").forEach(input => {
  input.addEventListener("blur", () => validateField(input));
  input.addEventListener("focus", () => clearError(input));
  if(input.id === "fullName") input.addEventListener("keydown", e => {
    setTimeout(() => greeting.textContent = "HOLA" + (input.value.trim() ? " " + input.value.trim() : ""), 0);
  });
});

function validateField(input) {
  const error = validators[input.id](input.value.trim());
  const group = input.closest('.form-group');
  const msg = group.querySelector('.error-message');
  const icon = group.querySelector('.icon');
  if(error) {
    group.classList.remove("success");
    group.classList.add("error");
    msg.textContent = error;
    fields[input.id] = false;
    if(icon) icon.setAttribute("data-status", "error");
  } else {
    group.classList.remove("error");
    group.classList.add("success");
    msg.textContent = "";
    fields[input.id] = true;
    if(icon) icon.setAttribute("data-status", "success");
  }
  if(input.id === "password" && document.getElementById("confirmPassword").value)
    validateField(document.getElementById("confirmPassword"));
}

function clearError(input) {
  const group = input.closest('.form-group');
  if(group.classList.contains("error")) {
    group.classList.remove("error");
    group.querySelector('.error-message').textContent = "";
    const icon = group.querySelector('.icon');
    if(icon) icon.setAttribute("data-status", "");
  }
}

form.addEventListener("submit", function(e) {
  e.preventDefault();
  let errores = [];
  form.querySelectorAll("input").forEach(input => {
    validateField(input);
    if(!fields[input.id]) {
      errores.push(input.previousElementSibling.textContent);
    }
  });

  if(errores.length) {
    alert("Por favor corrija los siguientes campos: " + errores.join(", "));
  } else {
    let datos = Array.from(form.elements)
      .filter(el => el.tagName === "INPUT")
      .map(el => el.previousElementSibling.textContent + ": " + el.value)
      .join("\n");
    alert("¡Formulario enviado correctamente!\n\n" + datos);
    form.reset();
    greeting.textContent = "Hola";
    form.querySelectorAll(".success").forEach(g => g.classList.remove("success"));
    // Limpia íconos
    form.querySelectorAll('.icon').forEach(i => i.setAttribute("data-status",""));
  }
});
