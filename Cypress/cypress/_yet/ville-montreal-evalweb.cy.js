describe('evalweb', () => {
  it('open', () => {


    cy.visit('https://servicesenligne2.ville.montreal.qc.ca/sel/evalweb/index')
    cy.get('.cky-notice-btn-wrapper > .cky-btn-accept').click()

    // carmel crescent
    // const props = [2,4,5,7,9]
    // const props = [10,11,12,13,15,16,17,18,19]
    // const props = [20,21,22,23,24,25,26,27,28,29]
    // const props = [30,31,32,33,34,35,36,37,38,39]
    // const props = [40,41,42,47,49,50,55,57]

    // sunrise crescent
    // const props = [2,4,5,6,7,8,9]
    // const props = [10,11,12,13,14,15,16,17,18,19]
    // const props = [20,21,22,23,24,25,26,27,28,29]
    // const props = [30,31,32,33,34,35,36,37,38,39]
    // const props = [40,41,42,43,44,45,46,47,48,49] 
    const props = [51,53,55]


    props.forEach(num => {
      var prop = new Property(num,'sunrise crescent')     

      cy.visit('https://servicesenligne2.ville.montreal.qc.ca/sel/evalweb/index')
      cy.get('.btn').click()
      cy.get('#noCiviq').type(prop.number)
      cy.get('#rue-tokenfield').type(prop.street).then(()=>{
        cy.wait(5000)
        cy.get('#ui-id-2').click()
        cy.get('#btnRechercher').click()
              
        cy.get('#section-2 > table > tbody > tr:nth-child(4) > th').invoke('text').then((purDate)=>{
          prop.purchaseDate = purDate.trim()
        })
  
        cy.get('#section-2 > table > tbody > tr:nth-child(1) > th').invoke('text').then((owner)=>{
          prop.owner = owner.trim().replace(',', '-').replace(/\s+/g, '-')
        })
        
        cy.get('#section-3 > table > tbody > tr:nth-child(3) > th:nth-child(2)').invoke('text').then((area)=>{
          prop.area = parseInt(area.replace('m2','').replace(',','').trim()/100)
        })
  
        cy.get('#section-4 > table > tbody > tr:nth-child(2) > th:nth-child(2)').invoke('text').then((refDate)=>{
          prop.referenceDate = refDate.trim()
        })
  
        cy.get('#section-4 > table > tbody > tr:nth-child(3) > th:nth-child(2)').invoke('text').then((landValue)=>{
          prop.landValue = landValue.replace('$','').trim().replace(/\s+/g, '')
        })
  
        cy.get('#section-4 > table > tbody > tr:nth-child(4) > th').invoke('text').then((buildValue)=>{
          prop.buildValue = buildValue.replace('$','').trim().replace(/\s+/g, '')
        })
  
        cy.get('#section-4 > table > tbody > tr:nth-child(5) > th').invoke('text').then((propValue)=>{
          prop.propValue = propValue.replace('$','').trim().replace(/\s+/g, '')
        })
        
        console.log(prop)
  
        // 
      })
      .then(()=>{
        cy.task('appendToCsv', {
          filePath: './output/ville-montreal-evalweb-output.csv',  // Specify the path to your CSV file
          content: objectToCsv(prop)
        }).then(() => {
          cy.log('Object appended to CSV file.');
        });
      })
    });

  })
})


class Property {
  constructor(number, street) {
    this.number = number;
    this.street = street;
    this.purchaseDate = ''
    this.owner = ''
    this.area = ''
  }
}

function objectToCsv(obj) {
  const csvRows = [];

  // // Get the headers (keys)
  // const headers = Object.keys(obj);
  // csvRows.push(headers.join(','));

  // Get the values
  const values = Object.values(obj);
  csvRows.push(values.join(','));

  return csvRows.join('\n');
}

const fs = require('fs');
const path = require('path');

