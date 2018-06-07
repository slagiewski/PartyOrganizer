export const pluralize = (word, count, suffix = 's') => {
  return `${count} ${word}${(count !== 1 ? suffix : '')}`
}