let callback = function () {
  let elements = document.getElementsByClassName("opblock-summary-description");

  for (const summaryDescription of elements) {
    const match = summaryDescription.textContent.match(/\[(\w|\s)+\]\s/);

    if (!match) {
      continue;
    }

    const trimmedTextContent = summaryDescription.textContent.replaceAll(match[0], "");
    summaryDescription.textContent = trimmedTextContent;

    const customTag = match[0].substring(1, match[0].length - 2);
    const summary = summaryDescription.parentElement;
    const customTagElement = document.createElement("div");

    customTagElement.innerText = customTag;
    customTagElement.className = "opblock-custom-tag";
    summary.appendChild(customTagElement);
  }
};

for (let attempts = 1; attempts <= 5; attempts++) {
  setTimeout(callback, attempts * 200);
}

function changeFavicon(newFaviconUrl) {
  const head = document.querySelector('head');
  const existingFavicon = document.querySelector('link[rel="icon"]');

  if (existingFavicon) {
    head.removeChild(existingFavicon);
  }

  const newFavicon = document.createElement('link');
  newFavicon.rel = 'icon';
  newFavicon.href = newFaviconUrl;

  head.appendChild(newFavicon);
}

changeFavicon('/icon/favicon.ico');
