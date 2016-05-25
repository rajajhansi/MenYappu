var gulp = require('gulp');
var less = require('gulp-less');
var path = require('path');

var config = {
    lessPath: './less',
    public: './public'
};

gulp.task('less', function () {
    return gulp.src(config.lessPath + '/**/*.less')
        .pipe(less({
            paths: [ path.join(__dirname, 'less', 'includes') ]
        }))
        .pipe(gulp.dest(config.public));
});


gulp.task('watch', function () {
    gulp.watch([
        config.lessPath + '/**/*.less'
    ], ['less']);
    console.log('watching for files');
});