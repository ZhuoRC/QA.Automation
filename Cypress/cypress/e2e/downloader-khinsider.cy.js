describe('Downloader', { taskTimeout: 300000 }, () => {
  it('https://downloads.khinsider.com/', () => {

    const albumUrl = "https://downloads.khinsider.com/game-soundtracks/album/worms-w.m.d-original-game-soundtrack-2016"
    
    // Extract directory name from URL
    const albumName = albumUrl.split('/album/')[1]
    
    cy.visit(albumUrl)
    cy.get("#songlist").find(".playlistDownloadSong > a").each((a) => {
      cy.wrap(a).invoke('attr', 'href').then(href => {

        const url = `https://downloads.khinsider.com/${href}`

        cy.log(url)
        cy.visit(url);
        cy.get("#audio").invoke('attr', 'src').then(mp3 => {
          console.log(mp3)
          const file = mp3.split("/")
          console.log(decodeURI(file[file.length - 1]))
          cy.task('downloadFile', {
            url: mp3,
            directory: albumName,
            fileName: decodeURI(file[file.length - 1])
          })
        })

      });

    })

  })
})