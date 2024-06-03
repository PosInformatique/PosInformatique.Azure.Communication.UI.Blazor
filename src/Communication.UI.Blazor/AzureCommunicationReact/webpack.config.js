const path = require('path');
const TerserPlugin = require("terser-webpack-plugin");

module.exports = {
    entry: './bundle.js',
    output: {
        path: path.resolve(__dirname, '../', process.env.AZURE_COMMUNICATION_REACT_BUNDLE_OUTPUT_PATH),
        filename: process.env.AZURE_COMMUNICATION_REACT_BUNDLE_FILE_NAME,
        libraryTarget: 'module',
        globalObject: 'this',
    },
    experiments: {
        outputModule: true
    },
    mode: 'production',
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /node_modules/
            }
        ]
    },
    optimization: {
        minimize: true,
        minimizer: [new TerserPlugin({
            terserOptions: {
                format: {
                    comments: false,
                },
            },
            extractComments: false,
        })],
    },
};