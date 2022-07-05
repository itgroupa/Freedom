window.getCaptcha = async function () {
    await grecaptcha.ready(function() {});

    const token = await grecaptcha.execute('6LfphcUgAAAAABpCdp8SoTXpEc1WkcuwOJ65b72R', {action: 'formSubmission'})

    return token;
}
