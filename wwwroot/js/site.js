/**
 * University Portal - Modern JavaScript Utilities
 * Author: Ahmed Elhosiny
 * Description: Enhanced UX features and utilities
 */

// ============================================
// Toast Notification System
// ============================================
const Toast = {
    show: function(message, type = 'info', duration = 3000) {
        const toastContainer = document.getElementById('toastContainer') || this.createContainer();
        const toast = document.createElement('div');
        toast.className = `toast align-items-center text-white bg-${type} border-0 show`;
        toast.setAttribute('role', 'alert');
        toast.innerHTML = `
            <div class="d-flex">
                <div class="toast-body">
                    <i class="fas fa-${this.getIcon(type)} me-2"></i>${message}
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
            </div>
        `;
        
        toastContainer.appendChild(toast);
        
        setTimeout(() => {
            toast.classList.remove('show');
            setTimeout(() => toast.remove(), 300);
        }, duration);
    },
    
    createContainer: function() {
        const container = document.createElement('div');
        container.id = 'toastContainer';
        container.className = 'toast-container position-fixed top-0 end-0 p-3';
        container.style.zIndex = '9999';
        document.body.appendChild(container);
        return container;
    },
    
    getIcon: function(type) {
        const icons = {
            'success': 'check-circle',
            'danger': 'exclamation-circle',
            'warning': 'exclamation-triangle',
            'info': 'info-circle'
        };
        return icons[type] || 'info-circle';
    }
};

// ============================================
// Loading Overlay
// ============================================
const Loading = {
    show: function() {
        const overlay = document.getElementById('loadingOverlay');
        if (overlay) {
            overlay.style.display = 'flex';
        }
    },
    
    hide: function() {
        const overlay = document.getElementById('loadingOverlay');
        if (overlay) {
            overlay.style.display = 'none';
        }
    }
};

// ============================================
// Form Validation Enhancement
// ============================================
function enhanceFormValidation() {
    const forms = document.querySelectorAll('.needs-validation');
    forms.forEach(form => {
        form.addEventListener('submit', function(event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
                Toast.show('Please fill in all required fields', 'warning');
            }
            form.classList.add('was-validated');
        }, false);
    });
}

// ============================================
// Smooth Scroll to Top
// ============================================
function createScrollToTop() {
    const scrollBtn = document.createElement('button');
    scrollBtn.innerHTML = '<i class="fas fa-arrow-up"></i>';
    scrollBtn.className = 'btn btn-primary rounded-circle position-fixed';
    scrollBtn.style.cssText = 'bottom: 20px; right: 20px; width: 50px; height: 50px; display: none; z-index: 1000; box-shadow: 0 4px 15px rgba(0,0,0,0.3);';
    scrollBtn.id = 'scrollToTop';
    
    scrollBtn.addEventListener('click', () => {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    });
    
    document.body.appendChild(scrollBtn);
    
    window.addEventListener('scroll', () => {
        if (window.pageYOffset > 300) {
            scrollBtn.style.display = 'block';
        } else {
            scrollBtn.style.display = 'none';
        }
    });
}

// ============================================
// Image Preview
// ============================================
function setupImagePreview(inputId, previewId) {
    const input = document.getElementById(inputId);
    const preview = document.getElementById(previewId);
    
    if (input && preview) {
        input.addEventListener('change', function(e) {
            const file = e.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function(e) {
                    preview.src = e.target.result;
                    preview.style.display = 'block';
                };
                reader.readAsDataURL(file);
            }
        });
    }
}

// ============================================
// Confirmation Dialog
// ============================================
function confirmAction(message, callback) {
    if (confirm(message)) {
        callback();
    }
}

// ============================================
// Data Table Enhancement
// ============================================
function enhanceDataTables() {
    const tables = document.querySelectorAll('.data-table');
    tables.forEach(table => {
        // Add hover effects
        const rows = table.querySelectorAll('tbody tr');
        rows.forEach(row => {
            row.style.cursor = 'pointer';
            row.addEventListener('click', function() {
                this.classList.toggle('table-active');
            });
        });
    });
}

// ============================================
// Auto-dismiss Alerts
// ============================================
function autoDismissAlerts(duration = 5000) {
    const alerts = document.querySelectorAll('.alert:not(.alert-permanent)');
    alerts.forEach(alert => {
        setTimeout(() => {
            const bsAlert = new bootstrap.Alert(alert);
            bsAlert.close();
        }, duration);
    });
}

// ============================================
// Search Debounce
// ============================================
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

// ============================================
// Local Storage Helper
// ============================================
const Storage = {
    set: function(key, value) {
        try {
            localStorage.setItem(key, JSON.stringify(value));
            return true;
        } catch (e) {
            console.error('Storage error:', e);
            return false;
        }
    },
    
    get: function(key) {
        try {
            const item = localStorage.getItem(key);
            return item ? JSON.parse(item) : null;
        } catch (e) {
            console.error('Storage error:', e);
            return null;
        }
    },
    
    remove: function(key) {
        localStorage.removeItem(key);
    },
    
    clear: function() {
        localStorage.clear();
    }
};

// ============================================
// Copy to Clipboard
// ============================================
function copyToClipboard(text) {
    navigator.clipboard.writeText(text).then(() => {
        Toast.show('Copied to clipboard!', 'success', 2000);
    }).catch(err => {
        Toast.show('Failed to copy', 'danger', 2000);
    });
}

// ============================================
// Initialize on DOM Ready
// ============================================
document.addEventListener('DOMContentLoaded', function() {
    // Hide loading overlay
    Loading.hide();
    
    // Create scroll to top button
    createScrollToTop();
    
    // Enhance form validation
    enhanceFormValidation();
    
    // Auto-dismiss alerts
    autoDismissAlerts();
    
    // Enhance data tables
    enhanceDataTables();
    
    // Add fade-in animation to cards
    const cards = document.querySelectorAll('.card');
    cards.forEach((card, index) => {
        card.style.opacity = '0';
        card.style.transform = 'translateY(20px)';
        setTimeout(() => {
            card.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
            card.style.opacity = '1';
            card.style.transform = 'translateY(0)';
        }, index * 100);
    });
    
    // Tooltips initialization
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
    
    // Popovers initialization
    const popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl);
    });
});

// ============================================
// Export functions for global use
// ============================================
window.Toast = Toast;
window.Loading = Loading;
window.Storage = Storage;
window.copyToClipboard = copyToClipboard;
window.confirmAction = confirmAction;
window.setupImagePreview = setupImagePreview;
window.debounce = debounce;
