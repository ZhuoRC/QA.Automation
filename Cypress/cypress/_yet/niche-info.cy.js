describe('niche', () => {
    it('niche.com', () => {
      const url = "https://www.niche.com/k12/polytechnic-school-pasadena-ca/"
      cy.visit(url, {
        failOnStatusCode: false,
        headers: {
            'user-agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36',
        }
    });

      // Handle Press & Hold anti-bot verification
      cy.get('body').then(($body) => {
        if ($body.find('.px-captcha-container').length > 0) {
          // Try iframe approach first
          cy.get('#px-captcha iframe').then(($iframe) => {
            if ($iframe.length > 0) {
              // Interact with the iframe container area
              cy.get('#px-captcha')
                .trigger('mousedown', { which: 1, force: true })
                .wait(3000)
                .trigger('mouseup', { force: true });
            }
          });
        }
      });
    })
  })