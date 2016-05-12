import {Aurelia} from 'aurelia-framework';
import {bootstrap} from 'aurelia-bootstrapper-webpack';

import 'bootstrap';
import '../node_modules/font-awesome/css/font-awesome.css';
import '../styles/styles.css';
import '../styles/kendo/2016.1/kendo.default.css';
import '../styles/winstrap/dist/css/winstrap.css';
import '../styles/prosody.css';
 
bootstrap((aurelia: Aurelia): void => {
  aurelia.use
    .standardConfiguration()
    .developmentLogging();

  aurelia.start().then(() => aurelia.setRoot('app', document.body));
});
