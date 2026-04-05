const CACHE = 'face-att-v1';
const STATIC = ['./index.html','./manifest.json'];

self.addEventListener('install',  e => e.waitUntil(caches.open(CACHE).then(c=>c.addAll(STATIC))));
self.addEventListener('activate', e => e.waitUntil(caches.keys().then(ks=>Promise.all(ks.filter(k=>k!==CACHE).map(k=>caches.delete(k))))));
self.addEventListener('fetch', e => {
  if (e.request.url.includes('/api/')) return;  // Never cache API calls
  e.respondWith(caches.match(e.request).then(cached => cached || fetch(e.request)));
});
