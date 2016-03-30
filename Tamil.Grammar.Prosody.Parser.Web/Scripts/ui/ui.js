(function($){

	$.fn.panelExpander = function (params) {
		var elem = $(this);
		var elemId = elem.attr('id');

		if (!$.trim(elemId)) {
			throw new Error('please provide an ID for the panel!');
		}

		// defaults
		var defaults = {
			toggler: elem.find('.stacked-menus-toggler'),
			leftPanel: elem.find('.left-panelbar'),
			rightPanel: elem.find('.right-panelbar')
		};

		// overwriting the defaults if we provide the params
		var config = $.extend(defaults, params);

		//TODO[burs]: This will need some refactoring if we're gonna have multiple panels

		var collapsedCssClass = 'panel-collapsed';
		var collapsedLocalStorageState = 'collapsed';
		var localStorageIdentifier = 'pagepanel-' + elem.attr('id');
		var isCollapsedOnLoad = localStorage.getItem(localStorageIdentifier) === collapsedLocalStorageState;

		if (config.onLoad) {
			config.onLoad(isCollapsedOnLoad, elem);
		}

		if (isCollapsedOnLoad) {
			elem.addClass(collapsedCssClass);
		}

		config.toggler.on('click', function (e) {
			if (elem.hasClass(collapsedCssClass)) {
				if (config.beforeExpand) {
					config.beforeExpand(e, elem);
				}

				elem.removeClass(collapsedCssClass);
				localStorage.removeItem(localStorageIdentifier);
				
				if (config.afterExpand) {
					config.afterExpand(e, elem);
				}
			} else {
				if (config.beforeCollapse) {
					config.beforeCollapse(e, elem);
				}

				elem.addClass(collapsedCssClass);
				localStorage.setItem(localStorageIdentifier, collapsedLocalStorageState);

				if (config.afterCollapse) {
					config.afterCollapse(e, elem);
				}
			}
		});
	};

	$.fn.panelSubmenus = function (params) {
		var elem = $(this);

		var defaults = {
			menus: elem.find(".stacked-menus-items"),
			submenuClass: 'stacked-menus-items-submenu'
		};

		var config = $.extend(defaults, params);

		config.menus.each(function (idx, val) {
			var currMenuElem = $(val);
			var hasSubmenu = currMenuElem.find('.' + config.submenuClass).length > 0;
			if (hasSubmenu) {
				currMenuElem.addClass('with-submenu');
			}
		});

		elem.on('click', '.with-submenu > a', function (e) {
			$(e.currentTarget).closest('.with-submenu').toggleClass('expanded');
			e.preventDefault();
		});
	};

})(jQuery);