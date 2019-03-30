var HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
    mode: 'development',
    externals: {        
        config: JSON.stringify({
            apiUrl: 'http://localhost:4000'
        })
    }
}