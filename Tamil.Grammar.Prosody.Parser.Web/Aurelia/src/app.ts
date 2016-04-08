import {Aurelia} from 'aurelia-framework';
import {Router, RouterConfiguration} from 'aurelia-router';

export class App {
  router: Router;
  
  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = 'Aurelia';
    config.map([
      { route: ['', 'welcome'], name: 'welcome',      moduleId: 'welcome',      nav: true, title: 'Welcome' },
      { route: 'introduction',  name: 'introduction', moduleId: './parts/introduction', nav: true, title: 'Introduction' },
      { route: 'letter',  name: 'letter', moduleId: './parts/letter', nav: true, title: 'letter' },
      { route: 'metreme',  name: 'metreme', moduleId: './parts/metreme', nav: true, title: 'metreme' },
      { route: 'metricalfoot',  name: 'metricalfoot', moduleId: './parts/metricalfoot', nav: true, title: 'metricalfoot' },
      { route: 'linkage',  name: 'linkage', moduleId: './parts/linkage', nav: true, title: 'linkage' },
      { route: 'metricalline',  name: 'metricalline', moduleId: './parts/metricalline', nav: true, title: 'metricalline' },
      { route: 'ornament',  name: 'ornament', moduleId: './ornament', nav: true, title: 'ornament' },
      { route: 'users',         name: 'users',        moduleId: './users',        nav: true, title: 'Github Users' },
      { route: 'child-router',  name: 'child-router', moduleId: './child-router', nav: true, title: 'Child Router' }
    ]);

    this.router = router;
  }
}
