(function($){

	$.fn.panelExpander = function (params) {
		var elem = $(this);
		var elemId = elem.attr('id');

		if (!$.trim(elemId)) {
			throw new Error('please provide an ID for the panel!');
		}

		var collapsedCssClass = 'panel-collapsed';
		var collapsedLocalStorageState = 'collapsed';
		var localStorageIdentifier = 'pagepanel-' + elem.attr('id');
		var isCollapsedOnLoad = localStorage.getItem(localStorageIdentifier) === collapsedLocalStorageState;

		if (isCollapsedOnLoad) {
			elem.addClass(collapsedCssClass);
		}

		// defaults
		var defaults = {
			toggler: elem.find('.stacked-menus-toggler'),
			leftPanel: elem.find('.left-panelbar'),
			rightPanel: elem.find('.right-panelbar')
		};

		// overwriting the defaults if we provide the params
		var config = $.extend(defaults, params);

		config.toggler.on('click', function (e) {
			if (elem.hasClass(collapsedCssClass)) {
				elem.removeClass(collapsedCssClass);
				localStorage.removeItem(localStorageIdentifier);
			} else {
				elem.addClass(collapsedCssClass);
				localStorage.setItem(localStorageIdentifier, collapsedLocalStorageState);
			}
		});
	};

	$("#container-wrapper-with-panels").panelExpander();

})(jQuery);