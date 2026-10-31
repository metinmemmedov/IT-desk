document.addEventListener('DOMContentLoaded', function () {
  var d = document.getElementById('Description'), c = document.getElementById('cnt');
  if (d && c) { var f = function () { c.textContent = d.value.length; }; d.addEventListener('input', f); f(); }
  document.querySelectorAll('form[action*="ChangeStatus"]').forEach(function (f) {
    f.addEventListener('submit', function (e) { if (!confirm('Status dəyişdirilsin?')) e.preventDefault(); });
  });
});
