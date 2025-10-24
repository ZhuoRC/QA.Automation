// YouTube URLs to test
const YOUTUBE_URLS = [
  'https://www.youtube.com/watch?v=kDmUHCKmA8E',
  // Add more YouTube URLs here
  // 'https://www.youtube.com/watch?v=VIDEO_ID_2',
  // 'https://www.youtube.com/watch?v=VIDEO_ID_3',
];

describe('YouTube Video Download via ytdown.io', () => {
  const YTDOWN_BASE_URL = 'https://ytdown.io/en2/';

  it('should download YouTube videos', () => {
    YOUTUBE_URLS.forEach((url, index) => {
      cy.log(`Processing: ${url}`);

      // Clear cookies, session storage, and local storage before each download to avoid rate limiting
      cy.clearCookies();
      cy.clearLocalStorage();
      cy.window().then((win) => {
        win.sessionStorage.clear();
      });

      // Visit ytdown.io
      cy.visit(YTDOWN_BASE_URL);

      // Enter YouTube URL
      cy.get('input[type="text"], input[name*="url"], #url, #txt-url')
        .first()
        .clear()
        .type(url, { delay: 100 });

      // Click download/submit button
      cy.get('button[type="submit"], input[type="submit"], button:contains("Download"), .btn-submit')
        .first()
        .click();

      // Check if cooldown message appears
      cy.get('body').then(($body) => {
        if ($body.text().includes('Please wait') || $body.text().includes('seconds before downloading')) {
          cy.log('Cooldown detected - waiting 25 seconds...');
          cy.wait(25000);
        }
      });

      // Wait for processing
      cy.wait(8000);

      // Get video title
      let videoTitle = '';
      cy.get('h1, h2, h3, .video-title, [class*="title"]', { timeout: 15000 })
        .first()
        .invoke('text')
        .then((title) => {
          videoTitle = title.trim();
          cy.log(`Video title: ${videoTitle}`);
        });

      // Wait for download button and trigger click via JavaScript
      cy.get('button:contains("Download"), a:contains("Download"), .download-btn, .btn-download', { timeout: 30000 })
        .first()
        .should('exist')
        .then(($btn) => {
          // Remove disabled attribute and enable the button
          $btn.removeAttr('disabled');
          $btn.prop('disabled', false);
          $btn.css('opacity', '1');
          $btn.css('cursor', 'pointer');

          // Trigger click event via JavaScript
          $btn[0].click();
        });

      cy.log(`Download initiated for: ${url}`);

      // Wait 25 seconds between videos to avoid rate limiting (except for the last one)
      if (index < YOUTUBE_URLS.length - 1) {
        cy.log('Waiting 25 seconds before next video...');
        cy.wait(25000);
      }
    });
  });
});
