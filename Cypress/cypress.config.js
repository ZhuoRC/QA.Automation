const { defineConfig } = require("cypress");
const fs = require('fs');
const path = require('path');

module.exports = defineConfig({
  e2e: {
    setupNodeEvents(on, config) {
      // implement node event listeners here
      on('task', {
        appendToCsv({ filePath, content }) {
          fs.appendFileSync(filePath, content + '\n', 'utf8');
          return null;
        },
        downloadFile({ url, directory, fileName }) {
          const https = require('https');
          const http = require('http');
          
          return new Promise((resolve, reject) => {
            // Create directory if it doesn't exist
            const fullDir = path.join(config.downloadsFolder || './cypress/downloads', directory);
            if (!fs.existsSync(fullDir)) {
              fs.mkdirSync(fullDir, { recursive: true });
            }
            
            const filePath = path.join(fullDir, fileName);
            const file = fs.createWriteStream(filePath);
            
            const client = url.startsWith('https') ? https : http;
            
            client.get(url, (response) => {
              response.pipe(file);
              file.on('finish', () => {
                file.close();
                resolve({ filePath });
              });
            }).on('error', (err) => {
              fs.unlink(filePath, () => {}); // Delete the file on error
              reject(err);
            });
          });
        }
      });
      
      return config;
    },
  },
  viewportWidth: 1440,
  viewportHeight: 900,
});
