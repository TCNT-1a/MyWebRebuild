//site.js
function generateSlug(input) {
    let slug = input.toLowerCase();
    slug = slug.normalize('NFD').replace(/[\u0300-\u036f]/g, '').replace(/[đĐ]/g, m => m === 'đ' ? 'd' : 'D');
    slug = slug.replace(/[^a-z0-9\s-]/g, '');
    slug = slug.trim().replace(/[\s-]+/g, '-');
    return slug;
}