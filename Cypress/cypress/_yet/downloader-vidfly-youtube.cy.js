// YouTube URLs to test
const YOUTUBE_URLS = [
  'https://www.youtube.com/watch?v=HtXpiuhMIF8&t=28s',
  // Add more YouTube URLs here
  // 'https://www.youtube.com/watch?v=VIDEO_ID_2',
  // 'https://www.youtube.com/watch?v=VIDEO_ID_3',
];

describe('YouTube Video Download via vidfly.ai', () => {
  const VIDFLY_BASE_URL = 'https://vidfly.ai/youtube-video-downloader/';

  beforeEach(() => {
    // Ignore uncaught exceptions from the application (React errors, etc.)
    cy.on('uncaught:exception', (err, runnable) => {
      // Return false to prevent Cypress from failing the test
      return false;
    });
  });

  it('should download YouTube videos', () => {
    YOUTUBE_URLS.forEach((url, index) => {
      cy.log(`Processing: ${url}`);

      // Clear cookies, session storage, and local storage before each download to avoid rate limiting
      cy.clearCookies();
      cy.clearLocalStorage();
      cy.window().then((win) => {
        win.sessionStorage.clear();
      });

      // Visit vidfly.ai
      cy.visit(VIDFLY_BASE_URL);

      // Wait for page to stabilize
      cy.wait(2000);

      // Enter YouTube URL - break up the chain to handle React re-renders
      cy.get('input[type="text"], input[name*="url"], #url, #txt-url, input[placeholder*="URL"], input[placeholder*="url"]')
        .first()
        .as('urlInput');

      cy.get('@urlInput').clear();
      cy.get('@urlInput').type(url, { delay: 0, force: true });

      // Click search button (it's a div element)
      cy.get('div.cursor-pointer:contains("Search"), div:contains("Search"), button:contains("Search"), button[type="submit"]')
        .first()
        .click({ force: true });

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

it('download', function() {
  cy.visit('https://vidfly.ai/youtube-video-downloader/')
  cy.get('input.bg-white').click();
  cy.get('input.bg-white').type('https://www.youtube.com/watch?v=HtXpiuhMIF8&t=28s');
  cy.get('span.text-sm.text-white').click();
  cy.get('button.hover\\:bg-\\[\\#7E15F4\\] span.mantine-Button-label').click();
  cy.get('button.hover\\:bg-\\[\\#7E15F4\\] span.mantine-Button-label').click();
  
});
