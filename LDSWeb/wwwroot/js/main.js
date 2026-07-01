// ============================================================
// Lucknow Bhulekh Admin Panel — shared script
// ============================================================

function showToast(msg){
  var toastEl = document.getElementById('appToast');
  if(!toastEl) return;
  document.getElementById('toastMsg').textContent = msg;
  new bootstrap.Toast(toastEl).show();
}

document.addEventListener('DOMContentLoaded', function(){

  // mobile sidebar toggle
  var sidebarToggle = document.getElementById('sidebarToggle');
  var sidebar = document.querySelector('.sidebar');
  if(sidebarToggle && sidebar){
    sidebarToggle.addEventListener('click', function(){
      sidebar.classList.toggle('show');
    });
  }

  // sidebar collapse/toggle (e.g. on Data Entry page)
  var sidebarCollapseToggle = document.getElementById('sidebarCollapseToggle');
  var appLayout = document.querySelector('.app-layout');
  if(sidebarCollapseToggle && sidebar && appLayout){
    sidebarCollapseToggle.addEventListener('click', function(){
      if(window.innerWidth > 992) {
        appLayout.classList.toggle('sidebar-collapsed');
      } else {
        sidebar.classList.toggle('show');
      }
    });
  }

  // approve / reject actions (only present on approve-reject.html)
  document.querySelectorAll('[data-decide]').forEach(function(btn){
    btn.addEventListener('click', function(){
      var status = this.getAttribute('data-decide');
      var row = this.closest('tr');
      var statusCell = row.children[5];
      var cls = status === 'Approved' ? 'badge-approved' : 'badge-rejected';
      statusCell.innerHTML = '<span class="badge-status '+cls+'">'+status+'</span>';
      row.querySelectorAll('button').forEach(function(b){ b.disabled = true; });
      showToast('Application '+status.toLowerCase()+' successfully.');
    });
  });

  // create user form (only present on create-user.html)
  var createUserForm = document.getElementById('createUserForm');
  if(createUserForm){
    createUserForm.addEventListener('submit', function(e){
      e.preventDefault();
      showToast('User created successfully.');
      this.reset();
    });
  }

  // login form (only present on index.html)
  var loginForm = document.getElementById('loginForm');
  if(loginForm){
    loginForm.addEventListener('submit', function(e){
      e.preventDefault();
      window.location.href = 'dashboard.html';
    });
  }

  // signup form (only present on signup.html)
  var signupForm = document.getElementById('signupForm');
  if(signupForm){
    signupForm.addEventListener('submit', function(e){
      e.preventDefault();
      window.location.href = 'index.html';
    });
  }

  // logout buttons (any page inside dashboard shell)
  document.querySelectorAll('.js-logout').forEach(function(el){
    el.addEventListener('click', function(e){
      e.preventDefault();
      window.location.href = 'index.html';
    });
  });

  // row action dropdown items (View / Edit / Reset Password / Deactivate)
  document.querySelectorAll('.js-user-action').forEach(function(item){
    item.addEventListener('click', function(e){
      e.preventDefault();
      var action = this.getAttribute('data-action');
      var name = this.getAttribute('data-name');
      showToast(action + ' ' + name + (action === 'Deactivate' || action.indexOf('Reset') === 0 ? '.' : '\u2019s profile.'));
    });
  });

  // pagination clicks (demo only — highlights selected page)
  var pagination = document.getElementById('userPagination');
  if(pagination){
    pagination.querySelectorAll('.page-item:not(.disabled) .page-link').forEach(function(link){
      link.addEventListener('click', function(e){
        e.preventDefault();
        pagination.querySelectorAll('.page-item').forEach(function(li){ li.classList.remove('active'); });
        var li = this.closest('.page-item');
        if(/^\d+$/.test(this.textContent.trim())){
          li.classList.add('active');
        }
      });
    });
  }

});
