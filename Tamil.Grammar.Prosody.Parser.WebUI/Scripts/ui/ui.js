(function ($) {

    $.fn.panelExpander = function (params) {
        var elem = $(this);
        var elemId = elem.attr('id');

        if (!$.trim(elemId)) {
            throw new Error('please provide an ID for the panel!');
        }

        var _defaultCallbacks = {
            left: function () {
                return false;
            },
            right: function () {
                return false;
            },
            bottom: function () {
                return false;
            }
        };

        var defaults = {
            toggler: {
                left: elem.find('#stacked-menu-toggler-left'),
                right: elem.find('#stacked-menu-toggler-right'),
                bottom: elem.find('#stacked-menu-toggler-bottom')
            },
            panels: {
                left: elem.find('.left-panelbar'),
                center: elem.find('.center-panelbar'),
                right: elem.find('.right-panelbar'),
                bottom: elem.find('.bottom-panelbar')
            },
            callbacks: {
                beforeExpand: $.extend({}, _defaultCallbacks),
                afterExpand: $.extend({}, _defaultCallbacks),
                beforeCollapse: $.extend({}, _defaultCallbacks),
                afterCollapse: $.extend({}, _defaultCallbacks)
            },
            persistState: {
                left: false,
                right: false,
                bottom: false
            },
            // INFO[burs]: initial state
            collapsed: {
                left: true,
                right: true,
                bottom: true
            },
            collapsedText: {
                left: '',
                right: 'Help',
                bottom: ''
            }
        };

        var collapsedClasses = {
            left: 'panel-collapsed-left',
            right: 'panel-collapsed-right',
            bottom: 'panel-collapsed-bottom'
        };

        var config = $.extend(true, defaults, params);

        var localStorageIdentifier = 'pagepanel-' + elem.attr('id');
        if (!localStorage.getItem(localStorageIdentifier)) {
            localStorage.setItem(localStorageIdentifier, JSON.stringify({collapsed: config.collapsed}));
        }
        var isCollapsedOnLoad = {};

        // INFO[burs]: determining the initial state of each panel based on the persistence settings..
        $.each(config.persistState, function (position, persistState) {
            if (persistState) {
                isCollapsedOnLoad[position] = JSON.parse(localStorage.getItem(localStorageIdentifier))['collapsed'][position];
            } else {
                isCollapsedOnLoad[position] = config.collapsed[position];
            }
        });

        var _onExpandActions = function (position, event, ctx) {
            config.callbacks.beforeExpand[position](event, elem, ctx);
            elem.removeClass(collapsedClasses[position]);
            config.callbacks.afterExpand[position](event, elem, ctx);
        };
        var _onCollapseActions = function (position, event, ctx) {
            config.callbacks.beforeCollapse[position](event, elem, ctx);
            elem.addClass(collapsedClasses[position]);
            config.callbacks.afterCollapse[position](event, elem, ctx);
        };
        var _handlePersistence = function (position, isCollapsing) {
            if (config.persistState[position]) {
                var panelState = JSON.parse(localStorage.getItem(localStorageIdentifier));
                panelState['collapsed'][position] = isCollapsing;
                localStorage.setItem(localStorageIdentifier, JSON.stringify(panelState));
            }
        };

        if (config.onLoad) {
            config.onLoad(isCollapsedOnLoad, elem);
        }

        // INFO[burs]: setting the initial state of the panels..
        $.each(isCollapsedOnLoad, function (position, value) {
            if (value) {
                elem.addClass(collapsedClasses[position]);
            }
        });

        // INFO[burs]: attaching the event handlers for every toggler ("hamburger" buttons)..
        $.each(config.toggler, function (position, togglerElement) {
            togglerElement.on('click', function (event) {
                var isExpanding = elem.hasClass(collapsedClasses[position]);
                if (isExpanding) {
                    _onExpandActions(position, event, this);
                } else {
                    _onCollapseActions(position, event, this);
                }
                _handlePersistence(position, !isExpanding);
            });
        });

        // INFO[burs]: adding the collapsed state text..
        $.each(config.collapsedText, function (position, panelTextOnCollapsed) {
            if ($.trim(panelTextOnCollapsed)) {
                var textElem = $("<div class='panel-text-on-collapsed-wrap'><div class='panel-text-on-collapsed'>" + panelTextOnCollapsed + "</div></div>");

                $(config['panels'][position]).append(textElem);
            }
        })
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
                var chevron = $("<span></span>");
                chevron.addClass('subitems-toggler glyph glyph-chevron-down');

                currMenuElem.addClass('with-submenu')
                    .append(chevron);
            }
        });

        elem.on('click', '.with-submenu > a', function (e) {
            $(e.currentTarget).closest('.with-submenu')
                .toggleClass('expanded')
                .children('.subitems-toggler')
                .toggleClass('glyph-chevron-down')
                .toggleClass('glyph-chevron-up');

            e.preventDefault();
        });
    };

    $.highlightCurrentPageInMenu = function () {
        var currentPageUrl = window.location.href;

        $(".page-link").each(function (idx, anchorTag) {
            var jqAnchorTag = $(anchorTag);
            var linkUrl = jqAnchorTag.attr('href');
            var found = currentPageUrl.indexOf(linkUrl) > -1;

            if (found) {
                jqAnchorTag.addClass('selected');
                return false;
            }
        });
    };

})(jQuery);